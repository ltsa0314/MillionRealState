# MillionRealState

Proyecto de gestión inmobiliaria desarrollado en .NET 8, con arquitectura por capas y despliegue mediante Docker Compose.  
Incluye autenticación y autorización robusta basada en **ASP.NET Core Identity** y **JWT**.

---

## Requisitos

- .NET 8 SDK
- Docker y Docker Compose

---

## Seguridad

La API implementa autenticación y autorización mediante **ASP.NET Core Identity** y **JWT (JSON Web Token)**:

- **ASP.NET Core Identity** gestiona usuarios, roles, contraseñas y políticas de acceso.
- Los usuarios pueden registrarse y autenticarse; al iniciar sesión se genera un token JWT.
- Los endpoints protegidos requieren el envío del token JWT en el encabezado `Authorization: Bearer {token}`.
- La configuración de Identity y JWT se realiza en el proyecto API, siguiendo las mejores prácticas de seguridad en .NET 8.
- Se pueden definir roles y políticas para controlar el acceso a recursos específicos.

---

## Levantar el proyecto con Docker Compose

1. **Clona el repositorio:**
~~~
git clone https://github.com/ltsa0314/MillionRealState.git cd MillionRealState
~~~

2. **Configura la cadena de conexión en `appsettings.json`**
(Por defecto, el proyecto usa SQL Server en un contenedor Docker)

~~~
  "ConnectionStrings": {
    "MillionRealStateDb": "Server=millionrealstate-db,1433;Database=MillionRealStateDb;User Id=sa;Password=MilliionPassswoord1!;TrustServerCertificate=True;",
  }
~~~


3. **Levanta los servicios con Docker Compose:**
~~~
docker-compose up --build
~~~
Esto iniciará:
- La base de datos SQL Server
- La API MillionRealState

4. **Aplicar migraciones (si es necesario):**
Puedes ejecutar las migraciones desde el contenedor de la API:
~~~
docker-compose exec api dotnet ef database update
~~~

5. **Accede a la API:**
La API estará disponible en [http://localhost:5000](http://localhost:5000) (o el puerto configurado en el `docker-compose.yml`).

---

## Ejecución de pruebas unitarias

Desde la raíz del proyecto, ejecuta:
~~~
dotnet test
~~~


## Estructura del proyecto

```text
MillionRealState/
├── src/
│   ├── MillionRealState.Domain/
│   ├── MillionRealState.Application/
│   ├── MillionRealState.Infrastructure/
│   └── MillionRealState.API/
├── MillionRealState.TestsUnit/
│   ├── Domain/
│   ├── Application/
│   ├── Infrastructure/
│   ├── API/
│   └── Mocks/
└── docker-compose.yml
```