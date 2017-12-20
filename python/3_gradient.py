import numpy as np
import timeit

def gradient_algorithm(f , f_grad , x0 , step , PREC , ITE_MAX ): # .......................................
	x = np.copy(x0)
	x_tab = np.copy(x)

	k = 0
	tolerance = PREC * np.linalg.norm(f_grad(x))
	while(k < ITE_MAX and np.linalg.norm(f_grad(x)) > tolerance):
		x = x -  step * f_grad(x)
		x_tab = np.vstack(( x_tab, x ))
		k += 1  
	
	return x,x_tab




def newton_algorithm(f , f_grad_hessian , x0 , PREC , ITE_MAX ): # .......................................
	x = np.copy(x0)
	x_tab = np.copy(x)

	return x,x_tab







def gradient_adaptive_algorithm(f , f_grad , x0 , step , PREC , ITE_MAX ): # .......................................
	x = np.copy(x0)
	x_tab = np.copy(x)

	return x,x_tab




	
