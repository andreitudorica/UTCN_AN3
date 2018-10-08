#include<iostream>
#include <iomanip>
using namespace std;
#define rdtsc __asm __emit 0fh __asm __emit 031h
#define cpuid __asm __emit 0fh __asm __emit 0a2h

void measure_add()
{
	unsigned cycles_high1 = 0, cycles_low1 = 0, cpuid_time = 0;
	unsigned cycles_high2 = 0, cycles_low2 = 0;
	unsigned __int64 temp_cycles1 = 0, temp_cycles2 = 0;
	__int64 total_cycles = 0;
	//compute the CPUID overhead
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		sub eax, cycles_low1
		mov cpuid_time, eax
		popad
	}
	cycles_high1 = 0;
	cycles_low1 = 0;
	//Measure the code sequence
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
	}

	for (int i = 0; i<1000; i++)
		{
			//add registers
			__asm {
				add ebx, ecx
			}
		}

	//Section of code to be measured
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high2, edx
		mov cycles_low2, eax
		popad
	}
	temp_cycles1 = ((unsigned __int64)cycles_high1 << 32) | cycles_low1;
	temp_cycles2 = ((unsigned __int64)cycles_high2 << 32) | cycles_low2;
	total_cycles = temp_cycles2 - temp_cycles1 - cpuid_time;
	cout <<"a."<< total_cycles / 1000 << " cycles " << " over 2.6 GHz gives " << fixed << setprecision(16) << (float)total_cycles / 1000 / 2600000000 << " seconds "<<'\n';

}
void measure_add_variable()
{
	unsigned cycles_high1 = 0, cycles_low1 = 0, cpuid_time = 0;
	unsigned cycles_high2 = 0, cycles_low2 = 0;
	unsigned __int64 temp_cycles1 = 0, temp_cycles2 = 0;
	__int64 total_cycles = 0;
	unsigned __int32 variable = 10;
	//compute the CPUID overhead
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		sub eax, cycles_low1
		mov cpuid_time, eax
		popad
	}
	cycles_high1 = 0;
	cycles_low1 = 0;
	//Measure the code sequence
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
	}

	for (int i = 0; i<1000; i++)
	{
		//add with variable
		_asm {
		add ebx, variable
		}
	}

	//Section of code to be measured
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high2, edx
		mov cycles_low2, eax
		popad
	}
	temp_cycles1 = ((unsigned __int64)cycles_high1 << 32) | cycles_low1;
	temp_cycles2 = ((unsigned __int64)cycles_high2 << 32) | cycles_low2;
	total_cycles = temp_cycles2 - temp_cycles1 - cpuid_time;
	cout << "b." << total_cycles / 1000 << " cycles " << " over 2.6 GHz gives " << fixed << setprecision(16) << (float)total_cycles / 1000 / 2600000000 << " seconds " << '\n';

}
void measure_mul()
{
	unsigned cycles_high1 = 0, cycles_low1 = 0, cpuid_time = 0;
	unsigned cycles_high2 = 0, cycles_low2 = 0;
	unsigned __int64 temp_cycles1 = 0, temp_cycles2 = 0;
	__int64 total_cycles = 0;
	unsigned __int32 variable = 10;
	//compute the CPUID overhead
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		sub eax, cycles_low1
		mov cpuid_time, eax
		popad
	}
	cycles_high1 = 0;
	cycles_low1 = 0;
	//Measure the code sequence
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
	}

	for (int i = 0; i<1000; i++)
	{
		//mul
		_asm {
		mul cx
		}
	}
	//Section of code to be measured
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high2, edx
		mov cycles_low2, eax
		popad
	}
	temp_cycles1 = ((unsigned __int64)cycles_high1 << 32) | cycles_low1;
	temp_cycles2 = ((unsigned __int64)cycles_high2 << 32) | cycles_low2;
	total_cycles = temp_cycles2 - temp_cycles1 - cpuid_time;
	cout << "c." << total_cycles / 1000 << " cycles " << " over 2.6 GHz gives " << fixed << setprecision(16) << (float)total_cycles / 1000 / 2600000000 << " seconds " << '\n';
}
void measure_fdiv()
{
	unsigned cycles_high1 = 0, cycles_low1 = 0, cpuid_time = 0;
	unsigned cycles_high2 = 0, cycles_low2 = 0;
	unsigned __int64 temp_cycles1 = 0, temp_cycles2 = 0;
	__int64 total_cycles = 0;
	unsigned __int32 variable = 10;
	//compute the CPUID overhead
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		sub eax, cycles_low1
		mov cpuid_time, eax
		popad
	}
	cycles_high1 = 0;
	cycles_low1 = 0;
	//Measure the code sequence
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
	}

	for (int i = 0; i<1000; i++)
	{
		//fdiv
		__asm {
		fdiv
		}

		/*_asm {
		fsub
		}*/
		//}
	}

	//Section of code to be measured
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high2, edx
		mov cycles_low2, eax
		popad
	}
	temp_cycles1 = ((unsigned __int64)cycles_high1 << 32) | cycles_low1;
	temp_cycles2 = ((unsigned __int64)cycles_high2 << 32) | cycles_low2;
	total_cycles = temp_cycles2 - temp_cycles1 - cpuid_time;
	cout << "d." << total_cycles / 1000 << " cycles " << " over 2.6 GHz gives " << fixed << setprecision(16) << (float)total_cycles / 1000 / 2600000000 << " seconds " << '\n';
}
void measure_fsub()
{
	unsigned cycles_high1 = 0, cycles_low1 = 0, cpuid_time = 0;
	unsigned cycles_high2 = 0, cycles_low2 = 0;
	unsigned __int64 temp_cycles1 = 0, temp_cycles2 = 0;
	__int64 total_cycles = 0;
	unsigned __int32 variable = 10;
	//compute the CPUID overhead
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		sub eax, cycles_low1
		mov cpuid_time, eax
		popad
	}
	cycles_high1 = 0;
	cycles_low1 = 0;
	//Measure the code sequence
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
	}

	for (int i = 0; i<1000; i++)
	{
		_asm {
		fsub
		}
	}

	//Section of code to be measured
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high2, edx
		mov cycles_low2, eax
		popad
	}
	temp_cycles1 = ((unsigned __int64)cycles_high1 << 32) | cycles_low1;
	temp_cycles2 = ((unsigned __int64)cycles_high2 << 32) | cycles_low2;
	total_cycles = temp_cycles2 - temp_cycles1 - cpuid_time;
	cout << "e." << total_cycles / 1000 << " cycles " << " over 2.6 GHz gives " << fixed << setprecision(16) << (float)total_cycles / 1000 / 2600000000 << " seconds " << '\n';
}
void measure_sort_static()
{
	int n = 100;
	int nums[100] = {};
	for (int i = 0; i < n; i++) 
	{
		int j = rand() % n;
		nums[j] = j;
	}

	unsigned cycles_high1 = 0, cycles_low1 = 0, cpuid_time = 0;
	unsigned cycles_high2 = 0, cycles_low2 = 0;
	unsigned __int64 temp_cycles1 = 0, temp_cycles2 = 0;
	__int64 total_cycles = 0;
	unsigned __int32 variable = 10;
	//compute the CPUID overhead
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		sub eax, cycles_low1
		mov cpuid_time, eax
		popad
	}
	cycles_high1 = 0;
	cycles_low1 = 0;
	//Measure the code sequence
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
	}

	bool p;
	do
	{
		p = false;
		for (int i = 1; i < n; i++)
		{
			if (nums[i - 1] > nums[i])
			{
				int aux = nums[i];
				nums[i] = nums[i - 1];
				nums[i - 1] = aux;
				p = true;
			}
		}
	} while (p == true);


	//Section of code to be measured
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high2, edx
		mov cycles_low2, eax
		popad
	}
	temp_cycles1 = ((unsigned __int64)cycles_high1 << 32) | cycles_low1;
	temp_cycles2 = ((unsigned __int64)cycles_high2 << 32) | cycles_low2;
	total_cycles = temp_cycles2 - temp_cycles1 - cpuid_time;
	cout << "2. sort static " << total_cycles << " cycles " << " over 2.6 GHz gives " << fixed << setprecision(16) << (float)total_cycles / 1000 / 2600000000 << " seconds " << '\n';
}
void measure_sort_dynamic()
{
	int n = 100;
	int *nums = (int*)malloc(sizeof(int*) * n);
	for (int i = 0; i < n; i++)
	{
		int j = rand() % n;
		nums[j] = j;
	}

	unsigned cycles_high1 = 0, cycles_low1 = 0, cpuid_time = 0;
	unsigned cycles_high2 = 0, cycles_low2 = 0;
	unsigned __int64 temp_cycles1 = 0, temp_cycles2 = 0;
	__int64 total_cycles = 0;
	unsigned __int32 variable = 10;
	//compute the CPUID overhead
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		popad
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
		pushad
		CPUID
		RDTSC
		sub eax, cycles_low1
		mov cpuid_time, eax
		popad
	}
	cycles_high1 = 0;
	cycles_low1 = 0;
	//Measure the code sequence
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high1, edx
		mov cycles_low1, eax
		popad
	}

	bool p;
	do
	{
		p = false;
		for (int i = 1; i < n; i++)
		{
			if (nums[i - 1] > nums[i])
			{
				int aux = nums[i];
				nums[i] = nums[i - 1];
				nums[i - 1] = aux;
				p = true;
			}
		}
	} while (p == true);


	//Section of code to be measured
	__asm {
		pushad
		CPUID
		RDTSC
		mov cycles_high2, edx
		mov cycles_low2, eax
		popad
	}
	temp_cycles1 = ((unsigned __int64)cycles_high1 << 32) | cycles_low1;
	temp_cycles2 = ((unsigned __int64)cycles_high2 << 32) | cycles_low2;
	total_cycles = temp_cycles2 - temp_cycles1 - cpuid_time;
	cout << "2. sort dynamic " << total_cycles  << " cycles " << " over 2.6 GHz gives " << fixed << setprecision(16) << (float)total_cycles / 1000 / 2600000000 << " seconds " << '\n';
}
int main()
{
	measure_add();
	measure_add_variable();
	measure_mul();
	measure_fdiv();
	measure_fsub();
	measure_sort_static();
	measure_sort_dynamic();
		return 0;
}