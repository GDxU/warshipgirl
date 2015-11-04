
#include "LibVLCWrapper.h"
#include <map>

extern "C"
{
	static void *lock(void *data, void **p_pixels)
	{
		*p_pixels = ((VlcWrapperData*)data)->buffer;
		return nullptr;
	}

	static void unlock(void *data, void *picture, void *const *p_pixels)
	{
		((VlcWrapperData*)data)->locked = false;
	}

	static libvlc_instance_t * libvlc;

	VLCWDLL_API int Init()
	{
		libvlc = libvlc_new(0, nullptr);
		if (!libvlc)
			return 0;
		return 1;
	}

	VLCWDLL_API void Destroy()
	{
		libvlc_release(libvlc);
	}

	VLCWDLL_API VlcWrapperData * Open(const char * URi)
	{
		libvlc_media_t * m = libvlc_media_new_path(libvlc, URi);
		if (!m) return nullptr;

		VlcWrapperData * vwd = new VlcWrapperData;
		if (!vwd) return nullptr;

		vwd->player = libvlc_media_player_new_from_media(m);
		if (!(vwd->player))
		{
			delete vwd;
			return nullptr;
		}

		vwd->length = libvlc_media_get_duration(m);
		libvlc_video_get_size(vwd->player, 0, &(vwd->width), &(vwd->height));
		vwd->fps = libvlc_media_player_get_fps(vwd->player);
		if ((vwd->fps < 1e-6) || (vwd->length == -1))
		{
			libvlc_media_player_release(vwd->player);
			delete vwd;
			return nullptr;
		}

		vwd->buffer = new char[vwd->width * vwd->height * 4 + (32 - (vwd->width * vwd->height * 4) % 32)];
		if (!(vwd->buffer))
		{
			libvlc_media_player_release(vwd->player);
			delete vwd;
			return nullptr;
		}

		libvlc_video_set_format(vwd->player, "RV32", vwd->width, vwd->height, vwd->width * 4);
		libvlc_video_set_callbacks(vwd->player, lock, unlock, nullptr, vwd);
		if (libvlc_media_player_play(vwd->player) == -1)
		{
			libvlc_media_player_release(vwd->player);
			delete[] vwd->buffer;
			delete vwd;
			return nullptr;
		}
		libvlc_media_release(m);

		libvlc_media_player_play(vwd->player);
		libvlc_media_player_pause(vwd->player);
		libvlc_media_player_set_position(vwd->player, 0);

		return vwd;
	}

	VLCWDLL_API void Close(VlcWrapperData * Instance)
	{
		libvlc_media_player_stop(Instance->player);
		libvlc_media_player_release(Instance->player);
		delete[] Instance->buffer;
		delete Instance;
	}

	VLCWDLL_API void * GetInfo(VlcWrapperData * Instance, Property p)
	{
		switch (p)
		{
		case V_WIDTH:
			return &Instance->width;
		case V_HEIGHT:
			return &Instance->height;
		case V_FPS:
			return &Instance->fps;
		case M_LENGTH:
			return &Instance->length;
		default:
			return nullptr;
		}
	}

	VLCWDLL_API void SetPosition(VlcWrapperData * Instance, time_t time)
	{
		Instance->locked = true;
		libvlc_media_player_set_position(Instance->player, ((float)time) / ((float)Instance->length));
	}

	VLCWDLL_API time_t GetPosition(VlcWrapperData * Instance)
	{
		return libvlc_media_player_get_time(Instance->player);
	}

	VLCWDLL_API void NextFrame(VlcWrapperData * Instance)
	{
		Instance->locked = true;
		libvlc_media_player_next_frame(Instance->player);
	}

	VLCWDLL_API char * GetFrame(VlcWrapperData * Instance)
	{
		while (Instance->locked);
		return Instance->buffer;
	}
}
