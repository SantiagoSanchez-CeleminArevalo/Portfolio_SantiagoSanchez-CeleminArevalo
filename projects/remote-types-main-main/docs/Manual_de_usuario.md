## Información Asignatura
### Asignatura: Sistemas Distribuidos
### Curso: 3º
---
## Datos
### Nombre: Santiago Sánchez-Celemín Arévalo
### Grupo Laboratorio: C2
### Outlook: Santiago.Sanchez12@alu.uclm.es
---
# Manual de Usuario (Remote-Types)

En este documento '.md' podrá encontrar el manual de usuario de este sistema distribuido que incluye un cliente para su mejor comprobación de los resultados, a su vez los pasos iniciales son aquellos para la instalación de dicho proyecto. El último fallo es el arreglo del test fallido y de su explicación.

## Paso 0
* El paso 0/Paso Inicial, sería instalar todo lo necesario para la ejecución de nuestro proyecto 'remote-types', para ello utilizamos el comando pertinente[1].

    [1] : pip install -e .

    ![alt text](Imagenes_manual\image0.png)

## Paso 1
 * En el paso 1, debemos de Inicializar nuestro servidor que ha de ser inicialmente predefinido, mediante el comando [1], posteriormente, ha de ser ejecutado desde la carpeta 'remote-types-main' el comando [2], ahí nos mostrará un proxy que debemos de ponerlo al ejecutar el cliente.

    [1] : remotetypes.Endpoints=tcp -p 10000

    [2] : remotetypes --Ice.Config=config/remotetypes.config

![alt text](Imagenes_manual\image1.png)



## Paso 2
* Aquí debemos de ejecutar nuestro cliente que se encuentra en la dirección 'remote-types-main/remotetypes/Client' tras acceder a esa carpeta debemos de realizar el comando pertinente[1], añadiendo el proxy impreso por el servidor al final de la cadena, posteriormente se nos ejecutará nuestro servidor.

    [1] : python3 Client.py 'Proxy...'

    ![alt text](Imagenes_manual\image2.png)

## Paso 2 (Excepción falta de proxy)

* En el supuesto caso de que intentemos inicializar nuestro Cliente y no se nos olvide introducir el proxy, nos saltará una excepción[1], la cual no nos permitirá iniciar el cliente.

    ![alt text](Imagenes_manual\image2Except.png)

## Paso 3 (Rset)
* Una vez inicializado nuestro servidor, nos permitirá realizar las distintas pruebas.

* En primer lugar nos aparecerá que si queremos establecer un nombre/identificador[1]. 

* Si ponemos un nombre, el 'RSet' se quedará con el nombre/identificador que le hemos puesto[2].

* Si no ponemos un nombre se generará un identificador aleatorio mediante la función 'str(uuid.uuid4())

* A partir de ahí podremos realizar las distintas pruebas para saber si nuestro código cumple con los requisitos establecidos

[1]:

![alt text](Imagenes_manual\image72.png)

[2]:

![alt text](Imagenes_manual\image73.png)

## Paso 4 (Rlist)
* Una vez inicializado nuestro servidor, nos permitirá realizar las distintas pruebas.

* En primer lugar nos aparecerá que si queremos establecer un nombre/identificador[1]. 

* Si ponemos un nombre, el 'RSet' se quedará con el nombre/identificador que le hemos puesto[2].

* Si no ponemos un nombre se generará un identificador aleatorio mediante la función 'str(uuid.uuid4())

* A partir de ahí podremos realizar las distintas pruebas para saber si nuestro código cumple con los requisitos establecidos

[1]:

![alt text](Imagenes_manual\image74.png)


[2]:

![alt text](Imagenes_manual\image75.png)

## Paso 5 (RDict)
* Una vez inicializado nuestro servidor, nos permitirá realizar las distintas pruebas.

* En primer lugar nos aparecerá que si queremos establecer un nombre/identificador[1]. 

* Si ponemos un nombre, el 'RSet' se quedará con el nombre/identificador que le hemos puesto[2].

* Si no ponemos un nombre se generará un identificador aleatorio mediante la función 'str(uuid.uuid4())

* A partir de ahí podremos realizar las distintas pruebas para saber si nuestro código cumple con los requisitos establecidos

[1]:

![alt text](Imagenes_manual\image76.png)

[2]:

![alt text](Imagenes_manual\image77.png)


** COSAS A TENER EN CUENTA: 
 * Al ser un diccionario es necesario introducir una clave y cada clave un valor. Es decir, si consultamos la wiki de Python, vemos como a los diccionarios  se les introduce una clave y un valor.

 * Un Identificador es UNICO, no permite tener objetos con el mismo identificador

## Paso 6 (Arreglo de test (test_bad_instantiation))
* Tras acceder a 'Git/accions', compruebo que uno de los test no los pasa, si accedemos a ese test fallido, nos indica en uno de los comentarios superiores que nos indica este mensaje: '"""Set that only allows adding str objects."""' el cual nos dice que solo permite añadir objetos de tipo 'String', por ello si buscamos dicha comprobación no la encontramos por lo que tengo que programamarla para que no se nos cierre, si no, salga una excepción, para ello realizamos esta programación:


    ![alt text](Imagenes_manual\image6.png)

* En esta programación lo que hacemos es que comprobamos si hay argumentos, si los has avanzamos, cogemos el primer argumento, iteramos cada uno de los elementos y dentro del bucle comprobamos si el elemento es una cadena, si no es así enviamos una excepción. Si todo esto se cumplo entonces se ejecutará


## Paso 7 (Arreglo de errores)

* En esta sección mostrare los errones que me han salido a la hora de realizar el pylint y la página donde he buscado como solucionar esos errores

    - Error : En el ruff se ejecutaba el Cliente, para anular este, segun las librerias de ruff podemos hacer un Exclude del archivo que queremos usar # exclude = ["remotetypes/Client/Client.py",] : https://docs.astral.sh/ruff/settings/

     - W1514 : https://pylint.readthedocs.io/en/latest/user_guide/messages/warning/unspecified-encoding.html

     - W0707 : https://pylint.readthedocs.io/en/stable/user_guide/messages/warning/raise-missing-from.html

     - Nombres : https://pylint.pycqa.org/en/1.9/technical_reference/features.html?highlight=re

     - C0303 : https://pylint.readthedocs.io/en/stable/user_guide/messages/convention/trailing-whitespace.html

     - C0114 : https://pylint.readthedocs.io/en/latest/user_guide/messages/convention/missing-module-docstring.html

     - C0304 : https://pylint.readthedocs.io/en/stable/user_guide/messages/convention/missing-final-newline.html

     - C0411 : https://pylint.readthedocs.io/en/stable/user_guide/messages/convention/wrong-import-order.html

     - R1705 : https://pylint.pycqa.org/en/latest/user_guide/messages/refactor/no-else-return.html