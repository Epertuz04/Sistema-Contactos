#  Sistema de Gestión de Contactos

 Aplicación web ASP.NET Core MVC para la administración centralizada de contactos de Manolo Limitada



##  Descripción

Sistema administrativo desarrollado en **ASP.NET Core 8.0** que permite a los administradores de Manolo Limitada:

-  **Autenticarse** de forma segura
-  **Listar** todos los contactos registrados
-  **Crear** nuevos contactos con validaciones completas
-  **Editar** información de contactos existentes
-  **Eliminar** contactos con confirmación
-  **Calcular automáticamente** la edad basada en fecha de nacimiento



##  Tecnologías Utilizadas

- **Framework:** ASP.NET Core 8.0 MVC
- **Lenguaje:** C#
- **Base de Datos:** SQLite
- **ORM:** Entity Framework Core
- **Frontend:** Bootstrap 5, HTML5, CSS3, JavaScript
- **Autenticación:** Session-based con hash SHA256



##  Requisitos Previos

- **.NET 8.0 SDK** instalado ([descargar aquí](https://dotnet.microsoft.com/download/dotnet/8.0))
- **Git** (opcional, para clonar el repositorio)
- **Navegador web** moderno (Chrome, Firefox, Edge, Safari)



## Instalación y Ejecución

 Este proyecto está desarrollado en ASP.NET Core 8.0 MVC y requiere tener instalado el .NET 8 SDK en tu máquina. A continuación se detallan los pasos para abrirlo y ejecutarlo de forma local:

- Clonar el repositorio
 git clone https://github.com/Epertuz04/Sistema-Contactos.git  
 cd Sistema-Contactos

- Restaurar dependencias
 dotnet restore

- Aplicar migraciones de base de datos
 dotnet ef database update

- Ejecutar la aplicación
 dotnet run

- Abrir en el navegador
 Accede a la dirección:
 http://localhost:5285

- Si el puerto está ocupado, puedes cambiarlo en Program.cs o iniciar  con:
 dotnet run --urls="http://localhost:5000"

##  Credenciales por Defecto

La aplicación viene con un usuario administrativo pre-configurado:

| Campo | Valor |
|-------|-------|
| **Usuario** | `admin` |
| **Contraseña** | `admin123` |



---

##  Estructura del Proyecto

```
Sistema-Contactos/
├── Controllers/           # Controladores (AccountController, ContactosController)
├── Models/               # Entidades y ViewModels
├── Views/                # Vistas Razor (.cshtml)
│   ├── Account/         # Vistas de autenticación
│   ├── Contactos/       # Vistas del CRUD de contactos
│   └── Shared/          # Layout y componentes compartidos
├── Data/                # Contexto de BD y migraciones
├── Migrations/          # Historial de cambios de BD
├── wwwroot/             # Archivos estáticos (CSS, JS, Bootstrap)
├── Program.cs           # Configuración de la aplicación
├── appsettings.json     # Configuración de conexión
└── SistemaContactos.db  # Base de datos SQLite (se crea automáticamente)
```

---

##  Campos del Contacto

| Campo | Tipo | Validación |
|-------|------|-----------|
| **Cédula** | Texto | 5-20 caracteres (requerido) |
| **Nombre** | Texto | 2-100 caracteres (requerido) |
| **Apellidos** | Texto | 2-150 caracteres (requerido) |
| **Fecha de Nacimiento** | Fecha | Requerida |
| **Teléfono** | Texto | Formato válido (requerido) |
| **Dirección** | Texto | 5-250 caracteres (requerido) |
| **Edad** | Número | Calculado automáticamente (solo lectura) |

---

## Funcionalidades Principales

### Autenticación y Sesión
 Login seguro con hash SHA256
 Sesiones de 30 minutos de inactividad
 Cierre de sesión automático
 Información del usuario en la barra de navegación

### Listado de Contactos
 Tabla responsive con todos los contactos
 Muestra: Cédula, Nombre, Apellidos, Teléfono, Dirección y Edad
 Botones de acción (Editar, Eliminar)
 Mensaje si no hay contactos registrados

### Crear Contacto
 Formulario con validaciones en tiempo real
 Campos requeridos con indicación visual
 Mensaje de éxito después de guardar
 Redirección automática al listado

### Editar Contacto
 Formulario pre-llenado con datos actuales
 Validaciones iguales al crear
 Edad mostrada como solo lectura
 Botón para guardar cambios

### Eliminar Contacto
 Pantalla de confirmación con advertencia
 Muestra todos los datos del contacto antes de eliminar
 Botones Confirmar y Cancelar
 Log de operación

### Seguridad
 Validación de autenticación en todas las acciones
 Tokens anti-CSRF en formularios
 Manejo de errores robusto
 Logs de operaciones (creación, edición, eliminación)



##  Interfaz de Usuario

 **Diseño moderno** con gradientes y sombras
 **Navegación intuitiva** con navbar personalizada
 **Colores consistentes** (púrpura y azul)
 **Responsivo** en dispositivos móviles y desktop
 **Bootstrap Icons** para mejor visual
 **Alertas claras** de éxito y error



## Base de Datos
 La base de datos del sistema está compuesta por dos tablas principales:

 Usuarios: utilizada para la autenticación administrativa.

 Contactos: almacena la información de clientes (Cédula, Nombre, Apellidos, Fecha de nacimiento, Teléfono, Dirección). La edad se calcula dinámicamente a partir de la fecha de nacimiento y no se guarda como campo físico

### Tablas Principales
 Estas son las Tablas que se usaron en el sistema.

#### Usuarios
```sql
CREATE TABLE Usuarios (
    Id INTEGER PRIMARY KEY,
    NombreUsuario TEXT NOT NULL,
    Contraseña TEXT NOT NULL,
    Email TEXT,
    NombreCompleto TEXT,
    Activo BOOLEAN,
    FechaCreacion DATETIME
);
```

#### Contactos
```sql
CREATE TABLE Contactos (
    Id INTEGER PRIMARY KEY,
    Cedula TEXT NOT NULL,
    Nombre TEXT NOT NULL,
    Apellidos TEXT NOT NULL,
    FechaNacimiento DATETIME NOT NULL,
    Telefono TEXT NOT NULL,
    Dirección TEXT NOT NULL
);
```


## Solución de Problemas
 Si la aplicación no arranca, asegúrate de tener instalado el .NET 8 SDK y ejecuta dotnet restore para las dependencias. Si aparece "Puerto en uso", cambia el puerto en Program.cs o usa dotnet run --urls="http://localhost:5000". Para errores de base de datos, aplica migraciones con dotnet ef database update. Finalmente, revisa que la URL local (http://localhost:5285) esté activa en tu navegador.

### Error: "Puerto ya en uso"
```bash
# Cambiar el puerto en Program.cs o especificar otro:
dotnet run --urls="http://localhost:5000"
```

### Error: "Base de datos no existe"
```bash
# Aplicar migraciones:
dotnet ef database update
```

### Error: "No se encuentra dotnet"
- Verificar que .NET 8.0 SDK esté instalado
- Agregar a PATH: `C:\Program Files\dotnet`



## Seguridad - Checklist Antes de Producción

-  Cambiar credenciales de admin (usuario y contraseña)
-  Usar HTTPS en producción (configurar certificados SSL)
-  Cambiar cadena de conexión a SQL Server (no SQLite)
-  Configurar variables de entorno para secretos
-  Implementar backup de base de datos
-  Habilitar logging en producción
-  Configurar CORS si es necesario


##  Historial de Cambios

### Versión 1.0 (2026-08-14)
  Implementación completa del CRUD de contactos
  Sistema de autenticación seguro
  Base de datos SQLite
  Interfaz web moderna y responsive
  Validaciones completas
  Cálculo automático de edad


**Desarrollado Por: Erick Pertuz usando ASP.NET Core 8.0**
