#include<iostream>
#include<fstream>
using namespace std;

int main()
{
	ifstream f("Text.txt");
	ofstream g("Text1.txt");
	/*char ranks[100][100];
	int n,g,p,c,d;
	for (int i = 1; i <= 19; i++)
		f >> ranks[i];
	f >> n;
	for(int i=1;i<=n;i++)
	*/
	for (int i = 1; i <= 16; i++)
		g << " grupa" << i;
	g << '\n';
	for (int i = 1; i <= 8; i++)
		g << " pluton" << i;
	g << '\n';
	for (int i = 1; i <= 8; i++)
		g << " compania" << i;
	g << '\n';
	for (int i = 1; i <= 4; i++)
		g << " batalionul" << i;
	g << '\n';
	for (int i = 1; i <= 2; i++)
		g << " divizia" << i;
	g << '\n';
	/*for (int i = 1; i < n; i++)
	{
		g << "(equivalent \n \t" << x[i] << "\n \t(and \n \t \t soldat-gradat \n \t \t (= rank " << i << ") \n \t \t (all \n \t \t \t este-inferior-lui\n\t\t\t(or\n";
		for (int j = i+1; j <= n; j++)
			g << "\t\t\t\t" << x[j] << "\n";
		g << "\t\t\t)\n\t\t)\n\t)\n)\n";
	}
	for (int i = 1; i <= n; i++)
	{
		for (int j = 1; j <= n; j++)
			if (i != j)
				g << " (disjoint " << x[i] << ' ' << x[j] << " )\n";
		g << '\n';
	}*/
	return 0;
}/*

 (disjoint general-of-the-army general)

 (equivalent 
	cadet 
	(and 
		ranked-soldier 
		(= rank 1)
		(all 
			is-inferior-of 
			(or 
				
			)
		)		
	)
)
 */