# EntregaFinalCoderHouse2022
 EntregaFinalCoderHouse2022


* Traer Nombre.-
* Inicio de sesión.-
* Crear usuario.-
* Modificar Usuario.-
* Traer usuario.-
* Eliminar usuario.-
* Crear producto.-
* Modificar producto.-
* Eliminar producto.-
* Cargar Venta.-
* Eliminar venta.-
* Traer productos.-
* Traer Productos Vendidos.-
* Traer ventas.-

* Traer Nombre: Se debe enviar un JSON al front que contenga únicamente un string con el Nombre de la App a elección.

* Inicio de sesión: Se le pasa como parámetro el nombre del usuario y la contraseña, buscar en la base de datos si el usuario existe y si coincide con la contraseña lo devuelve, caso contrario devuelve error.

* Crear usuario: Recibe como parámetro un JSON con todos los datos cargados y debe dar un alta inmediata del usuario con los mismos validando que todos los datos obligatorios estén cargados, por el contrario devolverá error (No se puede repetir el nombre de usuario. Pista... se puede usar el "Traer Usuario" si se quiere reutilizar para corroborar si el nombre ya existe).

* Modificar usuario: Se recibirán todos los datos del usuario por un JSON y se deberá modificar el mismo con los datos nuevos (No crear uno nuevo).

* Traer Usuario: Debe recibir un nombre del usuario, buscarlo en la base de datos y devolver todos sus datos (Esto se hará para la página en la que se mostrara los datos del usuario y en la página para modificar sus datos).

* Eliminar Usuario: Recibe el ID del usuario a eliminar y lo deberá eliminar de la base de datos.

* Crear producto: Recibe una lista de tareas por JSON, número de Id 0, Descripción , costo, precio venta y stock.

* Modificar producto: Recibe un producto con su número de Id, debe modificarlo con la nueva información.

* Eliminar producto: Recibe el número de Id de un producto a eliminar y debe eliminarlo de la base de datos.

* Cargar Venta: Recibe una lista de productos y el número de IdUsuario de quien la efectuó, primero cargar una nueva venta en la base de datos, luego debe cargar los productos recibidos en la base de ProductosVendidos uno por uno por un lado, y descontar el stock en la base de productos por el otro.

* Eliminar Venta: Recibe una venta con su número de Id, debe buscar en la base de Productos Vendidos cuáles lo tienen eliminándolos, sumar el stock a los productos incluidos, y eliminar la venta de la base de datos.

* Traer Productos: Debe traer todos los productos cargados en la base.

* Traer Productos Vendidos: Traer Todos los productos vendidos de un Usuario, cuya información está en su producto (Utilizar dentro de esta función el "Traer Productos" anteriormente hecho para saber que productosVendidos ir a buscar).

* Traer Ventas: Debe traer todas las ventas de la base, incluyendo sus Productos, cuya información está en ProductosVendidos.
