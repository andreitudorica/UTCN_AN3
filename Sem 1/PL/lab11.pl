%d_search(Start, Path)
d_search(X,_):- df_search(X,_).
d_search(_,L):- collect_v([],L).
df_search(X,L):-
asserta(vert(X)),
edge(X,Y),
\+(vert(Y)),
df_search(Y,L).
collect_v(L,P):-retract(vert(X)),!, collect_v([X|L],P).
collect_v(L,L).