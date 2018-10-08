% A1B2 : O colectie de predicate node(pt. noduri izolate) si edge(edge-clause)
:-dynamic edge/2.
:-dynamic node/1.
edge(a,b).
edge(b,c).
edge(b,d).
edge(c,d).
edge(a,d).
edge(b,a).
edge(c,b).
edge(d,b).
edge(d,c).
edge(d,a).

%edge(X,Y) :- edge1(X,Y);edge1(Y,X).

% A2B2 : O colectie de predicate neighbor(neighbor list-clause)

% declarăm predicatul dinamic pentru a putea folosi retract
:-dynamic neighbor/2.
%neighbor(a,[b,d]).
%neighbor(b,[a,c,d]).
%neighbor(c,[b,d]).
%neighbor(d,[a,b,c]).

% A1B1: O pereche formată dintr-o listă de noduri și o listă de muchii (graph-term)
%graph([a,b,c,d,e,f,g,h], [e(a,b), e(b,a), … ]).

% A2B1: O listă de perechi: nod, listă asociată de vecini (neighbor list-list)
list1([n(a, [b,d]), n(b, [a,c,d]), n(c, [b,d]), n(d, [a,b,c]), n(e,
[f,g]), n(f, [e]), n(g, [e]), n(h, [])]).

% Versiunea care distruge vechiul predicat
%neighb_to_edge:-retract(neighbor(Node,List)),!,
%				process(Node,List),
%				neighb_to_edge.
%neighb_to_edge.

%process(Node, [H|T]):- assertz(edge(Node, H)), process(Node, T).
%process(Node, []):- assertz(node(Node)).

% Versinua care nu distruge predicatul pe care se proceseaza
neighb_to_edge :- neighbor(Node,List),
				  process(Node,List),
				  fail.
neighb_to_edge.
process(Node, [H|T]):- assertz(edge(Node, H)),
					   process(Node, T).
process(Node, []):- assertz(node(Node)).


path(X,Y,Path):-path(X,Y,[X],Path).
path(X,X,PPath, PPath). 
path(X,Y,PPath, FPath):-edge(X,Z),
                      not(member(Z, PPath)),
                      path(Z, Y, [Z|PPath], FPath). 



optimal_path(X,Y,Path):-asserta(sol_part([],100)), path(X,Y,[X],Path,1).
optimal_path(,,Path):-retract(sol_part(Path,_)).
path(X,X,Path,Path,LPath):-
                        retract(sol_part(,)),!,
                        asserta(sol_part(Path,LPath)),
                        fail. 
path(X,Y,PPath,FPath,LPath):-edge(X,Z),
                              not(member(Z,PPath)),
                              LPath1 is LPath+1, 
                              sol_part(_,Lopt),
                              LPath1<Lopt, 
                              path(Z,Y,[Z|PPath],FPath,LPath1).
							  
edge_to_node :- retract(node(X)), !, assert(neighbor(X, [])), edge_to_node.
edge_to_node :- edge_to_neighb, !.

edge_to_neighb :- retract(edge(X, Y)), !,  procesare(X, Y), edge_to_neighb.
edge_to_neighb.

procesare(X, Y) :- retract(neighbor(X, Lis)), !, assert(neighbor(X, [Y|Lis])).
procesare(X, Y) :- assert(neighbor(X, [Y])).

hamilton(NN, X, Path):- NN1 is NN-1, hamilton_path(NN1,X, X, [X],Path).

hamilton_path(0, X, X, Path, Path) :- !.
hamilton_path(NN, X, Y, PPath, FPath) :- 
                          NN1 is NN-1, 
                          edge(X, Z), (Z=Y, NN1 = 0;\+member(Z, PPath)), 
                          hamilton_path(NN1, Z, Y, [Z|PPath], FPath). 


myLength([_|T], R) :- length(T, R1), R is R1 +1.
myLength([], 0).

cycle(X, [X|R]) :- edge(X, Y), 
                   path(Y, X, R1), 
                   myLength(R1, MyLength), 
                   MyLength>2, reverse(R1, R).



edge(a, b, 3). edge(b, a, 3).
edge(b, c, 4). edge(c, b, 4).
edge(c, d, 2). edge(d, c, 2).
edge(a, d, 10). edge(d, a, 10).


optimal_path_pondere(X, Y, Path) :- asserta(sol_part([], 100)), path_pondere(X, Y, [X], Path, 1).
optimal_path_pondere(_, _, Path) :- retract(sol_part(Path, _)).

path_pondere(X, X, Path, Path, LPath) :- retract(sol_part(_, _)), !, asserta(sol_part(Path, LPath)),fail.
path_pondere(X, Y, PPath, FPath, LPath) :- edge(X, Z, Cost), \+member(Z, PPath), LPath1 is LPath + Cost, sol_part(_, Lopt), LPath1 < Lopt, path_pondere(Z, Y, [Z|PPath], FPath, LPath1).