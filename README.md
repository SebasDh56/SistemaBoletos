# Sistema de Boletos

El **Sistema de Boletos** es una aplicación web diseñada para gestionar la venta de boletos de transporte en cooperativas como Imbaburapac y Lagos. Desarrollada en **ASP.NET Core** con **Entity Framework Core**, esta solución permite registrar personas, gestionar ventas, y mostrar resúmenes en tiempo real, todo con una interfaz moderna y amigable.

## Propósito del Proyecto

El objetivo principal del Sistema de Boletos es facilitar la administración de ventas de boletos para cooperativas de transporte. El sistema prioriza el uso de la cooperativa Imbaburapac (con una capacidad máxima de 45 boletos) antes de permitir ventas con Lagos, aplicando una comisión adicional cuando se usa esta última. Además, proporciona un resumen detallado de las ventas directamente en la página de registro, lo que permite a los usuarios tomar decisiones informadas sin necesidad de navegar a otras secciones.

## Funcionalidades Principales

### Registro de Personas
- Permite registrar personas con su nombre y cédula.
- Las personas registradas se usan para asociarlas a las ventas de boletos.
- Validaciones para evitar cédulas duplicadas o datos inválidos.

### Gestión de Ventas
- Registra ventas de boletos seleccionando una persona y una cooperativa.
- Aplica reglas de negocio:
  - Imbaburapac tiene prioridad hasta que se agoten sus 45 boletos (precio: $3.50 por boleto, sin comisión).
  - Si Imbaburapac está llena, se permite usar Lagos (precio: $3.15 por boleto, con una comisión de $0.35 por boleto para Imbaburapac).
- Calcula automáticamente el precio unitario y la comisión según la cooperativa seleccionada.
- Muestra mensajes de éxito o error al registrar una venta.

### Resumen de Ventas
- Integrado en la página de "Nueva Venta", muestra un resumen en tiempo real que incluye:
  - Boletos vendidos por cooperativa.
  - Boletos faltantes por cooperativa.
  - Total de ventas por cooperativa.
  - Precio promedio por boleto (ponderado).
  - Comisión total acumulada para Imbaburapac (por ventas de Lagos).
- Si no hay datos, muestra un mensaje informativo: "No hay datos de ventas disponibles".

### Interfaz de Usuario
- Diseño moderno y responsiva con Bootstrap 5.3.
- Colores armoniosos: tonos de azul (#2c3e50, #3498db) con acentos en rojo (#e74c3c).
- Tipografía clara: Segoe UI.
- Navegación intuitiva con enlaces a "Inicio", "Registrar Persona", y "Nueva Venta".

## Requisitos del Sistema

Para ejecutar este proyecto, necesitas:

- **.NET Core SDK 8.0** o superior.
- **SQL Server** (o cualquier base de datos compatible con Entity Framework Core).
- Visual Studio 2022 (recomendado) o cualquier IDE compatible.
- Conexión a internet para descargar dependencias de CDN (Bootstrap, Bootstrap Icons).

## Instalación

Sigue estos pasos para configurar el proyecto en tu máquina local:

### Clonar el Repositorio
1. Clona el repositorio desde GitHub:
   ```bash
   git clone https://github.com/tu-usuario/sistema-boletos.git
   cd sistema-boletos
   ```

### Configurar la Base de Datos
2. Crea una base de datos en SQL Server (por ejemplo, `SistemaBoletosDB`).
3. Actualiza la cadena de conexión en el archivo `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=SistemaBoletosDB;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```
4. Aplica las migraciones para generar las tablas necesarias:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

### Insertar Datos Iniciales
5. Inserta datos de prueba en las tablas para empezar a usar el sistema:
   ```sql
   INSERT INTO Personas (Id, Nombre, Cedula) VALUES (1, 'Jessica Tupiza', '1725390346');
   INSERT INTO Cooperativas (Id, Nombre, CapacidadMaxima) VALUES (1, 'Imbaburapac', 45);
   INSERT INTO Cooperativas (Id, Nombre, CapacidadMaxima) VALUES (2, 'Lagos', 100);
   ```

### Ejecutar el Proyecto
6. Inicia la aplicación:
   ```bash
   dotnet run
   ```
   - Abre tu navegador y accede a `https://localhost:5001` (o el puerto configurado en tu entorno).

## Uso del Sistema

### Registrar una Persona
1. Haz clic en "Registrar Persona" en la barra de navegación.
2. Completa el formulario con el nombre y la cédula de la persona.
3. Haz clic en "Registrar". Si todo es correcto, verás un mensaje de éxito.

### Registrar una Nueva Venta
1. Haz clic en "Nueva Venta".
2. Selecciona una persona y una cooperativa desde los menús desplegables.
3. Ingresa la cantidad de boletos que deseas vender.
4. Haz clic en "Registrar". El sistema verificará:
   - Si Imbaburapac tiene capacidad disponible (máximo 45 boletos).
   - Si estás usando Lagos, pero Imbaburapac aún tiene boletos disponibles (en ese caso, mostrará un error).
5. Si la venta se registra correctamente, verás un mensaje de éxito y el resumen de ventas se actualizará automáticamente.

### Ver el Resumen de Ventas
- En la página de "Nueva Venta", debajo del formulario, se muestra el resumen de ventas actualizado tras cada registro.
- Incluye estadísticas como boletos vendidos, faltantes, total de ventas, precio promedio, y comisiones.

## Estructura del Proyecto

El proyecto está organizado de la siguiente manera:

### Archivos Principales
- `Controllers/VentasController.cs`: Maneja la lógica de registro de ventas y generación de resúmenes.
- `Controllers/PersonasController.cs`: Controlador para registrar personas.
- `Models/Venta.cs`, `Models/Persona.cs`, `Models/Cooperativa.cs`: Modelos de datos para las entidades principales.
- `Data/AppDbContext.cs`: Configuración de Entity Framework Core para la base de datos.
- `Views/Shared/_Layout.cshtml`: Plantilla principal con el diseño de la interfaz.
- `Views/Ventas/Create.cshtml`: Vista para registrar ventas y mostrar el resumen.
- `Views/Personas/Create.cshtml`: Vista para registrar personas.

### Estilos y Diseño
- **Bootstrap 5.3**: Usado para la estructura responsiva y componentes básicos.
- **Bootstrap Icons**: Iconos para mejorar la experiencia visual.
- **CSS Personalizado**: Añade colores, sombras, y bordes redondeados para un diseño más estético.
- **Paleta de Colores**:
  - Fondo: #f0f4f8 (azul claro suave).
  - Navbar y Footer: Gradiente de #2c3e50 a #3498db (azul oscuro a azul claro).
  - Acentos: #e74c3c (rojo para enlaces activos o hover).
- **Tipografía**: Segoe UI para una mejor legibilidad.

## Solución de Problemas Comunes

### No se Registran las Ventas
- **Verifica los Datos Iniciales**: Asegúrate de tener al menos una persona y una cooperativa registradas en la base de datos.
- **Revisa los Errores**: Si ves un mensaje de error al registrar una venta, verifica los mensajes en la página o en la consola del servidor.
- **Conexión a la Base de Datos**: Confirma que la cadena de conexión en `appsettings.json` sea correcta y que SQL Server esté en ejecución.

### El Resumen No Muestra Datos
- Inserta datos de prueba en la tabla `Ventas`:
  ```sql
  INSERT INTO Ventas (PersonaId, CooperativaId, PrecioUnitario, CantidadBoletos, Fecha, ComisionImbaburapac) VALUES (1, 1, 3.50, 10, GETDATE(), 0.00);
  ```
- Recarga la página `/Ventas/Create` para ver el resumen actualizado.

### Errores de Compilación
- Asegúrate de tener instalada la versión correcta del .NET SDK.
- Restaura las dependencias:
  ```bash
  dotnet restore
  ```

## Contribuciones

Si deseas contribuir al proyecto:

1. Haz un fork del repositorio.
2. Crea una rama para tu cambio:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. Realiza tus cambios y haz commit:
   ```bash
   git commit -m "Añade nueva funcionalidad"
   ```
4. Sube tus cambios:
   ```bash
   git push origin feature/nueva-funcionalidad
   ```
5. Crea un Pull Request en GitHub con una descripción clara de tus cambios.

## Licencia

Este proyecto está bajo la **Licencia MIT**. Puedes usar, modificar, y distribuir el código libremente, siempre que incluyas la licencia original. Consulta el archivo `LICENSE` para más detalles.


---

© 2025 - Sistema de Boletos Cooperativos
