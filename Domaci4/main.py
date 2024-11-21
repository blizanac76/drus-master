import numpy as np
import matplotlib.pyplot as plt

import os


sada = os.path.dirname(os.path.abspath(__file__))

file_path = os.path.join(sada, "DomaciZadatak", "ConsoleApp1", "ConsoleApp1", "bin", "Debug", "random_brojevi3.txt")



#file_path = "\DomaciZadatak\ConsoleApp1\ConsoleApp1\bin\Debug\random_brojevi3.txt"

#otvaranje fajla sa read opcijom
try:
    with open(file_path, "r") as file:
        brojevi = np.array([int(line.strip()) for line in file])
except FileNotFoundError:
    print(f"File Not Found, lokacija: {file_path}")
    exit()
#Problem je bio sto je histogram za svaki broj pravio zaseban bin, stub na sebi i to je zauzimalo 16+ GB.
#Iako se gubi mozda na preciznosti, brojevi ce biti grupisani u 100 stubica, ali i dalje bi trebalo da prikaze realnu
#sliku raspodele
stubici = 100

plt.figure(figsize=(10, 6))
plt.suptitle("Histogram brojeva", fontsize=16)

plt.hist(brojevi, bins=stubici, color='blue', edgecolor='black', alpha=0.7)
plt.title("Uniformna rasporela novog LGCa", fontsize=16)
plt.xlabel("Broj", fontsize=14)
plt.ylabel("Ucestalost broja", fontsize=14)
plt.grid(axis='y', linestyle='--', alpha=0.7)
plt.tight_layout()

plt.show()

