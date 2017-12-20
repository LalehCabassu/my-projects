# IMPORTS
import numpy as np
from my_gradient import *


#################################################################
#################################################################
##### 
#####   II.1 Regression
##### 
#################################################################
#################################################################

#### File reading
dat_file = np.load('student.npz')
A_learn = dat_file['A_learn']
b_learn = dat_file['b_learn']
A_test = dat_file['A_test']
b_test = dat_file['b_test']

m = 395 # number of read examples (total:395)
n = 27 # features
m_learn = 300

x_reg = np.linalg.lstsq(A_learn,b_learn)[0] #   Numpy Least Square routine
#### 


###############
def s(x): 
	return 0.5*np.linalg.norm(  np.dot(  A_learn ,  x  ) -   b_learn   )**2 

def s_grad(x): # ....................................................
	A_learn_t = A_learn.transpose()	
	tmp = np.dot( A_learn_t, A_learn ) 
	g = np.dot(tmp, x) - np.dot( A_learn_t, b_learn )
	return g

def s_grad_hessian(x): # ....................................................
	g = 0
	H = 0
	return g,H


###############
ITE_MAX = 10000
PREC = 1e-10

A_learn_t = A_learn.transpose()	
L = np.linalg.norm(np.dot( A_learn_t, A_learn ) ) # ....................................................
step = 1 / L # ....................................................
x0 = np.zeros(n+1)

print(L)
print(step)

x,x_tab = gradient_algorithm(s , s_grad , x0 , step , PREC , ITE_MAX )
print("Difference with Numpy Least Square routine: {:e}  ".format( np.linalg.norm( x_reg - x )  ))


# d. Newton
#xn,xn_tab = newton_algorithm(s , s_grad_hessian , x0  , PREC , ITE_MAX )
#print("Difference with Numpy Least Square routine: {:e}  ".format( np.linalg.norm( x_reg - xn )  ))





################################
#print("\n\n#################\nGreat size\n#################\n")

mm = 10
nn = 500

A = np.random.randn(mm,nn)
b = np.random.randn(mm)


def s2(x):
	return 0.5*np.linalg.norm(  np.dot(  A,  x  ) -   b   )**2 

def s2_grad(x): # ....................................................
	g = 0 
	return g

def s2_grad_hessian(x): # ....................................................
	g = 0
	H = 0
	return g,H








