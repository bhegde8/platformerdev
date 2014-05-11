// RiceBall.cpp
//

#include "stdafx.h"

#include <iostream>
#include <SDL.h>



int _tmain(int argc, _TCHAR* argv[])
{
	if (SDL_Init(SDL_INIT_EVERYTHING) != 0){
		std::cout << "SDL_Init Error: " << SDL_GetError() << std::endl;
		return 1;
	}
	system("pause");

	SDL_Quit();

	return 0;
}

