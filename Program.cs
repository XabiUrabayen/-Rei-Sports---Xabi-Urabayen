namespace ReySports
{

    class Program
    {

        static void Main(string[] args)
        {
            // Punto de entrada del programa aranca el prgrama con el menu
            EjecutarPrograma();
        }


        // Lista general de pedidos
        static List<List<string>> pedidos = new List<List<string>>();
        // Muestra mensaje de error general
        static void MonstaraMensajeError(string mensaje)
        {
            Console.WriteLine($"[Error] {mensaje}");
        }

        // Limia todo lo que hay en pantalla pulsando una tecla
        static void Limpiezadechat()
        {
            Console.WriteLine("\nPresiona cualquier tecla para continuar....");
            Console.ReadKey();
            Console.Clear();
        }

        // Crear el pedido preguntado unos datos y lo guarda, despues muestra el resumen
        static void CrearPedido()
        {
            Console.WriteLine("\n---- Crear pedido ----");
            string nombreCliente = PedirNombreCliente();
            string tamañocaldazo = Pedirtamañocaldazo();
            List<string> listazapatillas = Seleccionarzapato();

            GuardarPedido(nombreCliente, tamañocaldazo, listazapatillas);

            MostrarResumenPedido(nombreCliente, tamañocaldazo, listazapatillas);
        }
        
        static string PedirNombreCliente()
        {
            Console.WriteLine("Nombre del cliente: ");
            return Console.ReadLine();
        }
        // Pide la talla del calzado y muestra un mensaje según la elección
        static string Pedirtamañocaldazo()
        {
            Console.WriteLine("\nQue talla de zapatillas usas ? ");
            Console.WriteLine("1. El 41");
            Console.WriteLine("2. El 42");
            Console.WriteLine("3. El 43");
            Console.WriteLine("4. El 44");
            Console.WriteLine("5. El 45");

            int opcion = GetEntrada("Opcion: ");
            string talla;

            if (opcion == 1) talla = "41";
            else if (opcion == 2) talla = "42";
            else if (opcion == 3) talla = "43";
            else if (opcion == 4) talla = "44";
            else if (opcion == 4) talla = "45";
            else talla = "No disponible";

            switch (talla)
            {
                case "41": Console.WriteLine("Talla comun."); break;
                case "42": Console.WriteLine("Muy solicitada"); break;
                case "43": Console.WriteLine("Pocas unidades"); break;
                case "44": Console.WriteLine("Grande, pocas unidades"); break;
                case "45": Console.WriteLine("Muy grande, pocas unidades"); break;
                default: Console.WriteLine("Talla no disponible"); break;
            }

            return talla;
        }

        // Valida que la entrada sea un numero entero
        static int GetEntrada(string mensaje)
        {
            int numero = 0;
            bool valido = false;

            while (!valido)
            {
                Console.WriteLine(mensaje);
                string entrada = Console.ReadLine();

                try
                {
                    numero = Convert.ToInt32(entrada); 
                    valido = true;
                }

                catch
                {
                    // Si falla, muestra error y depues el mensaje
                    MonstaraMensajeError("Fallo al procesar la entrada, intentalo de nuevo");
                }

            }
            return numero;
        }
        // Guarda el pedido en la lista global
        static void GuardarPedido(string nombre, string tamaño, List<string> cazalosdisponiles)
        {
            List<string> orden = new List<string> { nombre, tamaño };
            orden.AddRange(cazalosdisponiles); // Añade las zapatillas selecionadas
            orden.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); // Coloca la fehca y la hora

            pedidos.Add(orden); // Añade el pedido a la lista global
        }
        // Muestra un resumen del pedido recien creado
        static void MostrarResumenPedido(string nombre, string tamaño, List<string> cazalosdisponiles)
        {
            Console.WriteLine($"\nPedido creado para {nombre} - Tamaño: {tamaño}");
            Console.WriteLine("Ingredientes: " + string.Join(", ", cazalosdisponiles));
        }



        // Muesta todos los pedidos guardados
        static void MostrarPedido()
        {
            Console.WriteLine("\n ----Pedidos realizados----");

            if (pedidos.Count == 0)
            {
                Console.WriteLine("No hay pedidos");
                return;
            }



            foreach (var orden in pedidos)
            {
                string nombre = orden[0];
                string tamaño = orden[1];
                string fecha = orden[^1];
                List<string> cazalosdisponiles = orden.GetRange(2, orden.Count - 3);

                Console.WriteLine($"\nCliente: {nombre}");
                Console.WriteLine($"Calzado escogido: {tamaño}");
                Console.WriteLine($"Tamaño calzado: {string.Join(", ", cazalosdisponiles)}");
                Console.WriteLine($"Hora: {fecha}");
            }
        }

        // Se muestra el menu principal
        static void MostrarMenu()
        {
            Console.WriteLine("=== ReySports ===");
            Console.WriteLine("1. Crear pedido");
            Console.WriteLine("2. Ver pedidos");
            Console.WriteLine("3. Buscar pedido por cliente");
            Console.WriteLine("4. Salir");
        }


        // Permite seleccionar zapatilla de un catalogo
        static List<string> Seleccionarzapato()
        {
            Console.WriteLine("\n=== Selecionar Zapatillas ===");
            string[] disponibles =
            {
                "Nike Air force one",
                "Jordan 4 university blue",
                "Jordan 1",
                "Nike p6000",
                "Nike Air Max 95",
                "Nike Air Max 97"
            };
            List<string> seleccionados = new List<string>();

            while (true)
            {
                Mostrarcazalosdisponiles(disponibles);
                int opcion = GetEntrada("Opcion: ");

                if (opcion == 0) break;

                if (opcion >= 1 && opcion <= disponibles.Length)
                {
                    string zapatos = disponibles[opcion - 1];
                    if (!seleccionados.Contains(zapatos))
                    {
                        seleccionados.Add(zapatos);
                        Console.WriteLine($"{zapatos} añadido.");
                    }
                    else
                    {
                        Console.WriteLine($"{zapatos} ya esta selecionado.");
                    }
                }
                else
                {
                    MonstaraMensajeError("Opcion invalida.");
                }
            }

            return seleccionados;
        }

        // Muestra el catalogo de zapatillas disponibles
        static void Mostrarcazalosdisponiles(string[] zapatos)
        {
            Console.WriteLine("\nZapatillas dispobiles:");
            for (int i = 0; i < zapatos.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {zapatos[i]}");
            }
            Console.WriteLine("0. Terninasr selecion");
        }

        // Busca un pedido por el nombre de un cliente
        static void BuscarPedidoPorCliente()
        {
            Console.WriteLine("\nIntroduce el nombre del cliente a buscar:");
            string nombre = Console.ReadLine();
            bool encontrado = false;

            if (pedidos.Count == 0){
                Console.WriteLine("No hay pedidos");   
            }


            foreach (var pedido in pedidos)
            {
                if (pedido[0].ToLower() == nombre.ToLower())
                {
                    Console.WriteLine($"Cliente: {pedido[0]} -  Modelo: {pedido[2]} Talla: {pedido[1]} - Fecha y hora: {pedido[3]}");
                    encontrado = true;
                }

                else if (encontrado == false)
                {
                    Console.WriteLine("No se encontraron pedidos para este cliente.");
                }
            }
        }
            // Blucle principal del programa: muestra el menu y ejecuta las opciones elegidas
            static void EjecutarPrograma()
            {
                bool salir = false;

                while (!salir)
                {
                    MostrarMenu();
                    int opcion = GetEntrada("Seleciona una opcion");

                    if (opcion == 1)
                    {
                        CrearPedido();
                    }
                    else if (opcion == 2)
                    {
                        MostrarPedido();
                    }
                    else if (opcion == 3)
                    {
                        BuscarPedidoPorCliente();
                    }
                    else if (opcion == 4)
                    {
                        Console.WriteLine("Gracias por confiar en nosotros :)");
                        salir = true;
                    }
                    else
                    {
                        MonstaraMensajeError("No es escogido corectamente");
                    }

                    Limpiezadechat(); // Limpia la terminal tras cada accion
                }

            }
        
    }

}
