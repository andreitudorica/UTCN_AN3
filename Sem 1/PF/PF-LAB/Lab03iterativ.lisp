(defun lenl(l)
(
do(
(ls l (CDR ls))
(lg 0 (+ 1 lg))
)((ENDP ls) lg)))

(defun len_loop(l)
(setf lg 0)
(setf ls l)
(loop 
	(if (ENDP ls)(return lg))
	(setf lg (+ 1 lg))
	(setf ls (REST ls))
)
)

(defun cresc(l)
	(setf stat 1)
	(setf ls l)
	(loop
		(if (ENDP (rest ls)) (return stat))
		(if (> (first ls) (second ls)) (setf stat 0))
		(setf ls (REst ls))
	)
)

(defun elemente_numerice(l)
	(setf rez NIL)
	(setf ls l)
	(loop
		(if (ENDP ls) (return rez))
		(if (numberp (car ls)) (setf rez (append rez (list (car ls))))))
		(setf ls (REst ls))
	)
)