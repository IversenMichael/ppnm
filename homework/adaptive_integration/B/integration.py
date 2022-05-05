from scipy.integrate import quad
import numpy as np

def f(x):
    global ncalls
    ncalls = ncalls + 1
    return 1/np.sqrt(x)

def g(x):
    global ncalls
    ncalls = ncalls + 1
    return np.log(x)/np.sqrt(x)

print("The result from c# are compared with pythons standard integation library")
ncalls = 0
print(f"Integral of 1/sqrt(x) equals {quad(f, 0, 1)[0]}")
print(f"The function was called {ncalls} times")
ncalls = 0
print(f"Integral of ln(x)/sqrt(x) equals {quad(g, 0, 1)[0]}")
print(f"The function was calleod {ncalls} times")
