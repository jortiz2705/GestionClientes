# GestionClientes - Sistema de Administración Comercial

Una aplicación de consola desarrollada en **C# y .NET** orientada a la gestión, control financiero y reportería analítica de clientes en memoria. El sistema implementa arquitecturas limpias y buenas prácticas de diseño de software para garantizar la integridad y validación estricta de la información del negocio.

---

## Características Principales

El sistema ofrece un menú interactivo con las siguientes capacidades:
* **Registro Técnico de Clientes:** Validación en tiempo real para evitar duplicidad de IDs y protección contra valores financieros negativos al instanciar objetos.
* **Control de Crédito Avanzado:** Métodos seguros para el incremento y decremento de saldos, controlando que los montos sean válidos y que no existan sobregiros superiores al crédito disponible.
* **Gestión de Estados Inteligente:** Capacidad de activar o desactivar clientes. Las operaciones financieras y de edición se bloquean automáticamente si un cliente se encuentra en estado *Inactivo*.
* **Validación de Correo Electrónico:** Integración de la librería nativa `System.Net.Mail` para asegurar el formato real de los correos electrónicos mediante capturas de excepciones (`MailAddress`).
* **Reportería con LINQ:** Módulo analítico que calcula en tiempo real métricas clave: total de clientes, segmentos de estado (activos/inactivos), sumatorias de líneas de crédito globales, y valores de crédito máximos y mínimos.
* **Presentación Ordenada:** Listados dinámicos con clasificación prioritaria ordenada descendentemente por mayor crédito disponible y de forma alfabética por nombre.

---

## Conceptos Técnicos Demostrados

Este proyecto sirve como parte de mi portafolio para demostrar dominio en:
1. **Programación Orientada a Objetos (POO):** Encapsulamiento estricto utilizando propiedades con setters privados (`get; private set;`).
2. **Robustez y Manejo de Errores:** Control de flujo mediante excepciones personalizadas (`ArgumentException` y `FormatException`) para garantizar datos limpios antes de mutar el estado de los objetos.
3. **Programación Funcional (LINQ):** Uso avanzado de expresiones Lambda y métodos de extensión como `.FirstOrDefault()`, `.Count()`, `.Sum()`, `.Max()`, y `.OrderByDescending()`.
4. **Clean Code & Modularidad:** Separación de responsabilidades aislando la lógica del dominio (`Cliente.cs`) [1] de la lógica de presentación por consola y captura de datos (`Program.cs`).

---

## Estructura del Proyecto

* **`Cliente.cs`:** Clase del dominio que representa la entidad de negocio. Contiene las reglas del negocio, constructores y métodos mutadores de datos.
* **`Program.cs`:** Punto de entrada de la aplicación. Maneja el bucle principal de ejecución, impresión de interfaces de usuario en consola y métodos auxiliares de captura segura de teclado.

---

## Cómo Ejecutar el Proyecto

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com
   ```
2. **Abrir la solución:**
   Abre el archivo `GestionClientes.slnx` o abre la carpeta raíz desde **Visual Studio**.
3. **Compilar y Correr:**
   Presiona `F5` o haz clic en el botón de **Iniciar** en Visual Studio para desplegar la consola interactiva.

---

**Desarrollado por:** Jhon Ortiz Alvarado - https://www.linkedin.com/in/jhon-junior-ortiz-alvarado-224b7327/
