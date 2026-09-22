using GestionClientes;

List<Cliente> listaCliente = new List<Cliente>();
int opcion = 0;
while (opcion != 10)
{
    opcion = MostrarMenu();
    switch (opcion)
    {
        case 1:
            RegistrarCliente(listaCliente);
            break;
        case 2:
            ListarClientes(listaCliente);
            break;
        case 3:
            MostrarCliente(listaCliente);
            break;
        case 4:
            ActualizarCreditoPorId(listaCliente);
            break;
        case 5:
            DisminuirCreditoPorId(listaCliente);
            break;
        case 6:
            DesactivarCliente(listaCliente);
            break;
        case 7:
            ActivarCliente(listaCliente);
            break;
        case 8:
            ModificarCliente(listaCliente);
            break;
        case 9:
            MostrarReporteClientes(listaCliente);
            break;
        case 10:
            Console.WriteLine("Saliendo del programa...");
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}
///METODOS
static string ObtenerIdCliente()
{
    string ingresaId = "";
    while (true)
    {
        Console.Write("Ingrese ID del cliente: ");
        ingresaId = Console.ReadLine()?.ToUpper().Trim() ?? "";
        if (!string.IsNullOrWhiteSpace(ingresaId))
        {
            return ingresaId;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("El dato de Id del cliente no puede ser vacio");
        Console.ResetColor();
        Console.WriteLine("Por favor intente nuevamente");
    }
}
static string ObtenerNombreCliente()
{
    string nombreCliente;
    while (true)
    {
        Console.Write("Ingrese Nombre del cliente: ");
        nombreCliente = Console.ReadLine()?.ToUpper().Trim() ?? "";
        if (!string.IsNullOrWhiteSpace(nombreCliente))
        {
            return nombreCliente;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("El nombre del cliente no puede ser vacio");
        Console.ResetColor();
        Console.WriteLine("Por favor intente nuevamente");
    }
}
static string ObtenerCorreoCliente()
{
    string correoCliente;
    while (true)
    {
        Console.Write("Ingrese Correo del cliente: ");
        correoCliente = Console.ReadLine()?.ToUpper().Trim() ?? "";
        if (!string.IsNullOrWhiteSpace(correoCliente))
        {
            return correoCliente;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("El correo del cliente no puede ser vacio");
        Console.ResetColor();
        Console.WriteLine("Por favor intente nuevamente");
    }
}
static decimal ObtenerCredito()
{
    decimal creditoCliente;
    while (true)
    {
        Console.Write("Ingrese Credito del cliente: ");
        if (decimal.TryParse(Console.ReadLine(), out creditoCliente) && creditoCliente > 0)
        {
            return creditoCliente;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("El dato de crédito no es correcto");
        Console.ResetColor();
        Console.WriteLine("Por favor intente nuevamente");
    }
}
static bool ObtenerEstadoCliente()
{
    while (true)
    {
        Console.Write("El cliente esta activo (S/N): ");
        string? respuesta = Console.ReadLine()?.ToUpper().Trim() ?? "";
        if (respuesta == "S")
        {
            return true;
        }
        if (respuesta == "N")
        {
            return false;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ingrese estado correcto");
        Console.ResetColor();
        Console.WriteLine("Por favor intente nuevamente");
    }
}
static Cliente? BuscarCliente(List<Cliente> listaCliente, string idCliente)
{
    return listaCliente.FirstOrDefault(c => c.Id == idCliente);
}
static decimal ObtenerMonto()
{
    decimal monto;
    while (true)
    {
        Console.Write("Ingrese monto: ");
        if (decimal.TryParse(Console.ReadLine(), out monto) && monto > 0)
        {
            return monto;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("El dato del monto no es correcto");
        Console.ResetColor();
        Console.WriteLine("Por favor intente nuevamente");
    }
}
static void RegistrarCliente(List<Cliente> listaCliente)
{
    while (true)
    {
        string idCliente = ObtenerIdCliente();
        if (ExisteCliente(listaCliente, idCliente))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Id del cliente ya existe");
            Console.ResetColor();
            Console.WriteLine("Por favor intente nuevamente");
        }
        else
        {
            string nombreCliente = ObtenerNombreCliente();
            string correoCliente = ObtenerCorreoCliente();
            decimal creditoCliente = ObtenerCredito();
            bool estadoCliente = ObtenerEstadoCliente();
            try
            {
                Cliente cliente = new Cliente(idCliente, nombreCliente, correoCliente, creditoCliente, estadoCliente);
                listaCliente.Add(cliente);
                break;
            }
            catch (ArgumentException ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    } 
}
static void ListarClientes(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    int item = 0;

    var clientesOrdenados = listaCliente
        .OrderByDescending(c => c.Credito)
        .ThenBy(c => c.Nombre);
    foreach (var cliente in clientesOrdenados)
    {
        item++;
        MostrarDatosCliente(cliente);
    }
}
static void MostrarCliente(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    Cliente? clienteEncontrado = ObtenerClientePorId(listaCliente);
    if (clienteEncontrado == null)
    {
        return;
    }
    MostrarDatosCliente(clienteEncontrado);
}
static void ActualizarCreditoPorId(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    Cliente? clienteEncontrado = ObtenerClientePorId(listaCliente);
    if (clienteEncontrado == null)
    {
        return;
    }
    Console.WriteLine($"Cliente: {clienteEncontrado.Nombre}");

    decimal montoParaAumentar = ObtenerMonto();
    Console.WriteLine($"Crédito anterior: {clienteEncontrado.Credito:C2}");
    Console.WriteLine($"Monto aumentado: {montoParaAumentar:C2}");

    bool creditoActualizado = clienteEncontrado.ActualizarCredito(montoParaAumentar);
    if (creditoActualizado)
    {
        Console.WriteLine($"Nuevo crédito: {clienteEncontrado.Credito:C2}");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No se puede aumentar el crédito");
        Console.ResetColor();
    }
}
static void DisminuirCreditoPorId(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    Cliente? clienteEncontrado = ObtenerClientePorId(listaCliente);
    if (clienteEncontrado == null)
    {
        return;
    }
    Console.WriteLine($"Cliente: {clienteEncontrado.Nombre}");

    decimal montoParaDisminuir = ObtenerMonto();
    Console.WriteLine($"Crédito anterior: {clienteEncontrado.Credito:C2}");
    Console.WriteLine($"Monto a disminuir: {montoParaDisminuir:C2}");

    bool creditoActualizado = clienteEncontrado.DisminuirCredito(montoParaDisminuir);
    if (creditoActualizado)
    {
        Console.WriteLine($"Nuevo crédito: {clienteEncontrado.Credito:C2}");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No se puede disminuir el crédito");
        Console.ResetColor();
    }
}
static void ModificarCliente(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    Cliente? clienteEncontrado = ObtenerClientePorId(listaCliente);
    if (clienteEncontrado == null)
    {
        return;
    }
    Console.WriteLine("====Modificar Datos====");
    if (!clienteEncontrado.ActualizarNombre(ObtenerNombreCliente()))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("El dato del nombre no puede ser nulo");
        Console.ResetColor();
        return;
    }
        
    if (!clienteEncontrado.ActualizarCorreo(ObtenerCorreoCliente()))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ingrese un correo valido");
        Console.ResetColor();
        return;
    }
    //mostrar datos actualizados
    MostrarDatosCliente(clienteEncontrado);
}
static void ActivarCliente(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    Cliente? clienteEncontrado = ObtenerClientePorId(listaCliente);
    if (clienteEncontrado == null)
    {
        return;
    }
    while (true)
    {
        Console.Write("¿Desea Activar el cliente? (S / N): ");
        string? respuesta = Console.ReadLine()?.ToUpper().Trim() ?? "";
        if (respuesta == "S")
        {
            if (clienteEncontrado.Activar())
            {
                Console.WriteLine("Cliente Activado correctamente");
            }
            else
            {
                Console.WriteLine("Cliente ya se encuentra Activado");
            }
            break;
        }
        else if (respuesta == "N")
        {
            Console.WriteLine("Operación cancelada");
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ingrese respuesta correcta");
            Console.ResetColor();
            Console.WriteLine("Por favor intente nuevamente");
        }
    }
}
static void DesactivarCliente(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    Cliente? clienteEncontrado = ObtenerClientePorId(listaCliente);
    if (clienteEncontrado == null)
    {
        return;
    }
    while (true)
    {
        Console.Write("¿Desea desactivar el cliente? (S / N): ");
        string? respuesta = Console.ReadLine()?.ToUpper().Trim() ?? "";
        if (respuesta == "S")
        {
            if (clienteEncontrado.Desactivar())
            {
                Console.WriteLine("Cliente desactivado correctamente");
            }
            else
            {
                Console.WriteLine("Cliente ya se encuentra desactivado");
            }
            break;
        }
        else if (respuesta == "N")
        {
            Console.WriteLine("Operación cancelada");
            break;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ingrese respuesta correcto");
            Console.ResetColor();
            Console.WriteLine("Por favor intente nuevamente");
        }
    }
}
static bool HayClientesRegistrados(List<Cliente> listaCliente)
{
    return listaCliente.Count > 0;
}
static int MostrarMenu()
{
    int opcion;
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("===== GESTIÓN DE CLIENTES =====");
        Console.WriteLine("1. Registrar cliente");
        Console.WriteLine("2. Listar clientes");
        Console.WriteLine("3. Buscar cliente");
        Console.WriteLine("4. Aumentar crédito");
        Console.WriteLine("5. Disminuir crédito");
        Console.WriteLine("6. Desactivar cliente");
        Console.WriteLine("7. Activar Cliente");
        Console.WriteLine("8. Modificar Cliente");
        Console.WriteLine("9. Reporte de Clientes");
        Console.WriteLine("10. Salir");
        Console.Write("Seleccione una opción: ");
        if (int.TryParse(Console.ReadLine(), out opcion) && opcion >= 1 && opcion <= 10)
        {
            break;
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ingrese opción correcta");
        Console.ResetColor();
        Console.WriteLine("Por favor intente nuevamente");
    }
    return opcion;
}
static bool ExisteCliente(List<Cliente> listaCliente, string idCliente)
{
    return listaCliente.Any(c => c.Id == idCliente);
}
static void MostrarReporteClientes(List<Cliente> listaCliente)
{
    if (!ValidarClientesRegistrados(listaCliente))
    {
        return;
    }
    int totalClientes = listaCliente.Count;
    var clientesActivos = listaCliente.Count(c => c.Estado);
    int clientesInactivos = totalClientes - clientesActivos;
    var totalCredito = listaCliente.Sum(c => c.Credito);
    var maximoCredito = listaCliente.Max(c => c.Credito);
    var minimoCredito = listaCliente.Min(c => c.Credito);

    Console.WriteLine("========== REPORTE DE CLIENTES ==========");
    Console.WriteLine($"Total clientes: {totalClientes}");
    Console.WriteLine($"Clientes activos: {clientesActivos}");
    Console.WriteLine($"Clientes inactivos: {clientesInactivos}");

    Console.WriteLine($"Crédito total: {totalCredito:C2}");
    Console.WriteLine($"Crédito máximo: {maximoCredito:C2}");
    Console.WriteLine($"Crédito mínimo: {minimoCredito:C2}");
}
static bool ValidarClientesRegistrados(List<Cliente> listaCliente)
{
    if (!HayClientesRegistrados(listaCliente))
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No existen clientes registrados.");
        Console.ResetColor();
        return false;
    }
    return true;
}

static void MostrarDatosCliente(Cliente cliente)
{
    Console.WriteLine();
    Console.WriteLine("===== DATOS DEL CLIENTE =====\n");
    Console.WriteLine($"ID: {cliente.Id}");
    Console.WriteLine($"Nombre: {cliente.Nombre}");
    Console.WriteLine($"Email: {cliente.Email}");
    Console.WriteLine($"Crédito: {cliente.Credito:C2}");
    Console.WriteLine($"Estado: {(cliente.Estado ? "ACTIVO" : "INACTIVO")}");
    Console.WriteLine();
}

static Cliente? ObtenerClientePorId(List<Cliente> listaCliente)
{
    string idABuscar = ObtenerIdCliente();
    Cliente? clienteEncontrado = BuscarCliente(listaCliente, idABuscar);
    if (clienteEncontrado != null)
    {
        return clienteEncontrado;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Cliente no encontrado");
        Console.ResetColor();
        return null;
    }
}
/*
 static void ProbarWhere(List<Cliente> listaCliente)
{
    string? nombreCliente="";
    var clientesActivos = listaCliente.Where(c => c.Estado == true && c.Credito>1000);
    var clienteNombre = listaCliente.Where(c => c.Nombre == nombreCliente);
    foreach (var cliente in clientesActivos)
    {
        Console.WriteLine($"Cliente Id: {cliente.Id}"); 
    }
}
static void ProbarSelect(List<Cliente> listaCliente)
{
    var nombres = listaCliente
        .Where(c => c.Estado) //cuando comparamos un campo bool no es necesario colocar c.Estado == true
        .Select(c => new {c.Id, c.Nombre});
    foreach (var nombre in nombres) 
    {
        Console.WriteLine($"Nombres: {nombre}");
    }
}
static void ListarClientesActivos(List<Cliente> listaCliente)
{
    var clientesActivos = listaCliente.Where(c => c.Estado);
    foreach (var cliente in clientesActivos)
    {
        Console.WriteLine($"Id: {cliente.Id}");
        Console.WriteLine($"Nombre: {cliente.Nombre}");
        Console.WriteLine($"Estado: {(cliente.Estado ? "ACTIVO" : "INACTIVO")}");
    }
}
static void ListarClientesOrdenados(List<Cliente> listaCliente)
{
    var clientesOrdenados = listaCliente.OrderBy(c => c.Nombre);
    foreach (var cliente in clientesOrdenados)
    {
        Console.WriteLine($"Id: {cliente.Id}");
        Console.WriteLine($"Nombre: {cliente.Nombre}");
        Console.WriteLine($"Estado: {(cliente.Estado ? "ACTIVO" : "INACTIVO")}");
    }
}
 */
/*// 1. La expresión switch evalúa 'opcion' y guarda el método correspondiente en 'ejecutar'
    //Action: Es un delegado que apunta a un método que no devuelve nada (void).
    Action ejecutarAccion = opcion switch
    {
        1 => () => RegistrarCliente(listaCliente),
        2 => () => ListarClientes(listaCliente),
        3 => () => MostrarCliente(listaCliente),
        4 => () => ActualizarCreditoPorId(listaCliente),
        5 => () => Console.WriteLine("Saliendo del programa..."),
        _ => () => Console.WriteLine("")
    };
    // 2. Se ejecuta el método seleccionado en una sola línea
    ejecutarAccion();*/

//ActualizarCredito(cliente1, 1000);
//Console.WriteLine($"Crédito del cliente {cliente1.Id}: {cliente1.Credito:C2}");