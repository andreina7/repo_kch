using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto
{
    // Clase que representa un Producto con sus propiedades.
    class Producto
    {
        // Propiedades del Producto
        public int Id_producto { get; set; }  // Identificador único del producto
        public string Nom_prod { get; set; }  // Nombre del producto
        public decimal Valor { get; set; }  // Precio del producto

        // Constructor para inicializar un nuevo Producto
        public Producto(int id, string nombre, decimal precio)
        {
            Id_producto = id;
            Nom_prod = nombre;
            Valor = precio;
        }

        // Método ToString que sobrescribe la forma de mostrar el Producto
        // Se usa cuando queremos representar el Producto como cadena de texto
        public override string ToString()
        {
            return $"Producto: {Id_producto}, Nombre: {Nom_prod}, Precio: {Valor:C}";  // Formato de salida con el precio como moneda
        }
    }

    class Program
    {
        // Lista para almacenar los productos en memoria
        static List<Producto> productos = new List<Producto>();

        // Variable para generar IDs únicos de productos
        static int contadorId = 1;

        static void Main(string[] args)
        {
            try
            {
                while (true)
            {
                // Mostrar el menú principal
                Console.Clear();  // Limpiar la consola
                Console.WriteLine("Bienvenido al sistema de productos.");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar Producto");
                Console.WriteLine("2. Eliminar Producto");
                Console.WriteLine("3. Buscar Producto");
                Console.WriteLine("4. Mostrar Productos");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una Opción: ");

                string opcion = Console.ReadLine();  // Leer la opción seleccionada por el usuario

                // Procesar la opción seleccionada
                switch (opcion)
                {
                    case "1":
                        AgregarProducto();  // Llamar al método para agregar un producto
                        break;
                    case "2":
                        EliminarProducto();  // Llamar al método para eliminar un producto
                        break;
                    case "3":
                        BuscarProducto();  // Llamar al método para buscar productos
                        break;
                    case "4":
                        VerProductos();  // Llamar al método para mostrar productos
                        break;
                    case "5":
                        Console.WriteLine("Saliendo del sistema...");  // Salir del programa
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Intente de nuevo.");  // Opción inválida
                        break;
                }

                // Esperar que el usuario presione una tecla para continuar
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
            }
        }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        // Método para agregar un nuevo producto a la lista
        static void AgregarProducto()
        {
            // Solicitar el nombre del producto
            Console.Write("Ingrese el nombre del producto: ");
            string nombre = Console.ReadLine();

            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre del producto no puede estar vacío.");
                return;
            }

            decimal precio = 0;
            // Validar que el precio ingresado sea un número decimal válido
            while (true)
            {
                Console.Write("Ingrese el precio del producto: ");
                if (decimal.TryParse(Console.ReadLine(), out precio))  // Verificar si el precio es un número válido
                {
                    break;  // Si el precio es válido, salir del bucle
                }
                else
                {
                    // Mostrar mensaje de error si el precio no es válido
                    Console.WriteLine("Precio no válido. Por favor, ingrese un número decimal.");
                }
            }

            // Crear un nuevo producto con un ID único, nombre y precio
            Producto nuevoProducto = new Producto(contadorId++, nombre, precio);

            // Agregar el nuevo producto a la lista
            productos.Add(nuevoProducto);
            Console.WriteLine("Producto agregado exitosamente.");
        }

        // Método para eliminar un producto de la lista
        static void EliminarProducto()
        {
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos para eliminar.");
                return;
            }

            // Solicitar al usuario el Id_producto a eliminar
            Console.Write("Ingrese el Id producto a eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))  // Intentar convertir el ID ingresado a entero
            {
                // Buscar el producto con el Id_producto ingresado en la lista
                Producto productoAEliminar = productos.Find(p => p.Id_producto == id);

                // Si se encuentra el producto, eliminarlo de la lista
                if (productoAEliminar != null)
                {
                    productos.Remove(productoAEliminar);
                    Console.WriteLine("Producto eliminado exitosamente.");
                }
                else
                {
                    // Si no se encuentra el producto, mostrar mensaje de error
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            else
            {
                // Si el Id_producto ingresado no es válido, mostrar mensaje de error
                Console.WriteLine("Id_producto no válido.");
            }
        }

        // Método para buscar un producto por su nombre
        static void BuscarProducto()
        {
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos disponibles para buscar.");
                return;
            }

            // Solicitar al usuario el nombre del producto a buscar
            Console.Write("Ingrese el nombre del producto a buscar: ");
            string nombreBusqueda = Console.ReadLine().Trim();// eliminia espacios innecesarios

            if (string.IsNullOrEmpty(nombreBusqueda))  // Validar que el nombre no esté vacío
            {
                Console.WriteLine("El nombre del producto no puede estar vacío.");
                return;  // Salir del método si la búsqueda es inválida
            }
            // Buscar todos los productos cuyo nombre contenga la cadena de búsqueda (sin distinguir mayúsculas/minúsculas)
            List<Producto> productosEncontrados = productos.FindAll(p => p.Nom_prod.IndexOf(nombreBusqueda, StringComparison.OrdinalIgnoreCase) >= 0);

            // Si se encuentran productos, mostrarlos
            if (productosEncontrados.Count > 0)
            {
                Console.WriteLine("Productos encontrados:");
                foreach (var producto in productosEncontrados)
                {
                    Console.WriteLine(producto);  // Mostrar el producto
                }
            }
            else
            {
                // Si no se encuentran productos, mostrar mensaje de error
                Console.WriteLine("No se encontraron productos con ese nombre.");
            }
        }

        // Método para mostrar todos los productos almacenados en la lista
        static void VerProductos()
        {
            // Si no hay productos en la lista, mostrar mensaje
            if (productos.Count == 0)
            {
                Console.WriteLine("No hay productos disponibles.");
            }
            else
            {
                // Si hay productos, mostrar cada uno
                Console.WriteLine("Lista de productos:");
                foreach (var producto in productos)
                {
                    Console.WriteLine(producto);  // Mostrar el producto
                }
            }
        }
    }
}

