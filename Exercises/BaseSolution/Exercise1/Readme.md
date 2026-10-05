# Ejercicio 1: Principio de Responsabilidad Única (SRP)

**Principio:** Single Responsibility Principle (SRP)

## 📋 Descripción

Este ejercicio demuestra cómo aplicar el **Principio de Responsabilidad Única (SRP)** refactorizando código que viola este principio. El objetivo es separar las responsabilidades de infraestructura de la lógica de negocio.

## 🎯 Objetivo del Ejercicio

Entender que una clase debe tener **una única razón para cambiar**. En este caso:

- **`PingPongService4`**: Responsable únicamente de la lógica del juego
- **`PingPongHelper4`**: Responsable de acceder a los recursos (lectura de archivos, configuración)

## 📁 Estructura de la Solución

```
.
├── TheArtOfUnitTesting.Services/
│   ├── PingPongService4.cs       - Lógica del juego (Responsabilidad única)
│   └── PingPongHelper4.cs        - Acceso a infraestructura (Responsabilidad única)
│
├── TheArtOfUnitTesting.Services.UnitTests/
│   └── PingPongService4Tests.cs  - Tests unitarios
│
└── Readme.md                      - Este archivo
```

## 🔍 Análisis del Problema

### ¿Cuál es la responsabilidad de `PingPongService4`?

El servicio debe encargarse de:
- ✅ Aplicar la lógica del juego de Ping Pong
- ✅ Determinar el resultado basándose en la acción del usuario

### ⚠️ Problemas Identificados en `PingPongHelper4`

El archivo `PingPongHelper4.cs` contiene varios problemas documentados en el código:

```csharp
/// Problem1: Not Testeable
/// Problem2: Not Mockeable
/// Problem3: Not Maintainable
/// Problem4: What happens if we need parameters or dependencies in GetWin? 
///           More methods? More parameters? Until when?
/// Problem5: What happens if we need some domain service in Helper to resolve GetWin? 
///           How can we pass this dependency to helper? As a parameter? == Problem4
/// Problem6: Very very coupled to lot of classes and codes.
/// Problem7: It is not extensible.
```

Estos problemas revelan que el diseño actual tiene limitaciones que afectarán el mantenimiento y la evolución del código.

## 🏗️ Implementación Actual

### `PingPongService4.cs`

Clase de servicio que implementa la lógica del juego:
- Define constantes: `Ping`, `Pong`, `PingPong`, `Fault`
- Método principal: `GetPingPongGame(string action)`
- Valida parámetros de entrada
- Retorna resultados basados en la lógica del juego

**Responsabilidad única:** Lógica de negocio del juego de Ping Pong

### `PingPongHelper4.cs`

Clase estática helper que encapsula acceso a infraestructura:
- Método: `GetWin(string filePath)`
- Responsabilidad: Lectura de archivos
- Acepta `filePath` como parámetro

**Responsabilidad única:** Acceso a recursos de infraestructura (lectura de archivos)

## 🧪 Tests Unitarios

**Archivo:** `PingPongService4Tests.cs`

Los tests validan:
- ✅ Casos básicos con diferentes acciones ("Ping", "Pong", "PingPong")
- ✅ Casos de error (acciones inválidas como "AAAAA")
- ✅ Validación de argumentos nulos
- ✅ Patrón AAA (Arrange-Act-Assert)

**Casos de prueba:**
```csharp
[TestCase("Ping", "PingPong")]
[TestCase("Pong", "PingPong")]
[TestCase("PingPong", "PingPongWin")]
[TestCase("AAAAA", "Fault")]
```

## ✅ Beneficios del SRP en Esta Implementación

| Beneficio | Descripción |
|-----------|------------|
| **Claridad** | Cada clase tiene una responsabilidad claramente definida |
| **Testabilidad** | Fácil de testear sin dependencias complejas |
| **Mantenibilidad** | Cambios en la lógica del juego no afectan la lectura de archivos |
| **Responsabilidad única** | Cada clase tiene una razón para cambiar |
| **Separación de capas** | Lógica de negocio separada de infraestructura |

## 📚 Conceptos Clave del SRP

El **Single Responsibility Principle** establece que:

> Una clase debe tener una única razón para cambiar, lo que significa que debe tener una única responsabilidad.

En esta implementación:
- 🎮 **Lógica del juego** → `PingPongService4`
- 📁 **Acceso a archivos** → `PingPongHelper4`

Cada clase cambia solo si su responsabilidad específica cambia.

## 🔗 Relación con otros Principios SOLID

El SRP es fundamental para los otros principios SOLID:
- **OCP** (Open/Closed): Clases cerradas para modificación, abiertas para extensión
- **LSP** (Liskov Substitution): Facilita la sustitución correcta de implementaciones
- **ISP** (Interface Segregation): Interfaces específicas para cada responsabilidad
- **DIP** (Dependency Inversion): Inyección de dependencias para cada responsabilidad

## 🚀 Próximos Pasos

Para mejorar esta solución podrías considerar:
1. Crear una **interfaz** para `PingPongHelper4` para mejorar testabilidad
2. Usar **inyección de dependencias** para pasar el helper al servicio
3. Aplicar otros principios SOLID en ejercicios posteriores

## 📌 Notas Importantes

- La solución actual ya respeta el SRP
- El helper sigue siendo simple y enfocado
- Los problemas documentados en el helper sugieren la evolución hacia soluciones más avanzadas
- El parámetro `filePath` en `GetWin()` muestra una forma de mantener la flexibilidad
