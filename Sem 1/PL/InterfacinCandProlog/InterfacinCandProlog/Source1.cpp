# include <fstream>
# define inf 1000000000
using namespace std;
ifstream f("dijkstra.in");
ofstream g("dijkstra.out");
int x, y, c, i, n, m, ok;
struct drum { int x, y, c; }X[250003];
int D[50003];
int main()
{
	f >> n >> m;
	for (i = 1; i <= m; ++i)
	{
		f >> x >> y >> c;  X[i].x = x;  X[i].y = y;  X[i].c = c;
		if (x == 1)  D[y] = c;
	}
	for (i = 2; i <= n; ++i)  
		if (D[i] == 0)  
			D[i] = inf;
	do
	{
		ok = 1;
		for (i = 1; i <= m; ++i)
		{
			if (D[X[i].y] > D[X[i].x] + X[i].c)
			{
				D[X[i].y] = D[X[i].x] + X[i].c;
				ok = 0;
			}
		}
	} while (!ok);
	for (i = 2; i <= n; i++)
		if (D[i] != inf)g << D[i] << ' ';
		else g << "0 ";
		return 0;
}