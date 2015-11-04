
#ifndef _LIBVLCWRAPPER_H_
#define _LIBVLCWRAPPER_H_

#ifdef VLCWDLL_EXPORTS
#define VLCWDLL_API __declspec(dllexport)
#else
#define VLCWDLL_API __declspec(dllimport)
#endif

#pragma comment(lib, "libvlc.lib")

#include <vlc/vlc.h>

extern "C"
{
	enum Property
	{
		V_WIDTH,
		V_HEIGHT,
		V_FPS,
		M_LENGTH
	};

	typedef struct
	{
		char * buffer;
		libvlc_media_player_t * player;
		unsigned int width, height;
		time_t length;
		float fps;
		bool locked;
	}VlcWrapperData;

	VLCWDLL_API int Init();

	VLCWDLL_API void Destroy();

	VLCWDLL_API VlcWrapperData * Open(const char * URi);

	VLCWDLL_API void Close(VlcWrapperData * Instance);

	VLCWDLL_API void * GetInfo(VlcWrapperData * Instance, Property p);

	VLCWDLL_API void SetPosition(VlcWrapperData * Instance, time_t time);

	VLCWDLL_API time_t GetPosition(VlcWrapperData * Instance);

	VLCWDLL_API void NextFrame(VlcWrapperData * Instance);

	VLCWDLL_API char * GetFrame(VlcWrapperData * Instance);
}

#endif
