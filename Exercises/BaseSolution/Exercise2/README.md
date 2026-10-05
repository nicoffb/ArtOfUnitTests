# Exercise 2: Solución - Aplicando SOLID (DIP - Dependency Inversion Principle)

## 📋 Descripción del Ejercicio

Este ejercicio demuestra cómo refactorizar código acoplado usando clases estáticas hacia una solución que respete el **Dependency Inversion Principle (DIP)** de SOLID, permitiendo inyección de dependencias y facilidad para testing.

---

## ❌ El Problema: Código Original

### Análisis del código base (`BaseSolution/Exercise2`)

#### `PingPongHelper2.cs` (Clase Estática)

```csharp
public static class PingPongHelper2
{
    public static string GetWin()
    {
        var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
        return File.ReadAllText(filePath);
    }
}
```

#### `PingPongService2.cs` (Utilizando la clase estática)

```csharp
if (action == PingPong)
{
    return action + PingPongHelper2.GetWin();
}
```

### ⚠️ Problemas Identificados

| Problema | Descripción | Impacto |
|----------|------------|--------|
| **1. No Testeable** | No podemos instanciar la clase para hacer pruebas unitarias aisladas | Los tests siempre ejecutan el código real del método `GetWin()` |
| **2. No Mockeable** | No podemos crear un mock de `PingPongHelper2` para simular comportamientos | Imposible probar casos especiales (excepciones, diferentes valores, etc.) |
| **3. No Mantenible** | Si cambiamos la implementación, afecta toda la aplicación | Cambios temenidos y riesgo de regresiones |
| **4. No Escalable** | ¿Qué pasará si necesitamos parámetros o más dependencias en `GetWin()`? | Añadimos más métodos estáticos → más problemas |
| **5. Difícil de Inyectar Dependencias** | Las clases estáticas no permiten constructor, por lo que no podemos inyectar dependencias | Quedamos atrapados con las dependencias hardcodeadas |
| **6. Fuertemente Acoplado** | `PingPongService2` está directamente acoplado a `PingPongHelper2` | Cambios en uno requieren cambios en otro |
| **7. No Extensible** | No podemos extender o cambiar el comportamiento sin modificar el código original | Viola el principio Open/Closed de SOLID |

### ¿Por qué una clase estática no es extensible?

Una clase estática **no se puede instanciar**, lo que significa:
- ❌ No podemos crear subclases que hereden de ella
- ❌ No podemos pasar una interfaz a través del constructor
- ❌ No podemos mockear los métodos para testing
- ❌ No podemos aplicar polimorfismo
- ❌ **No cumple con Dependency Inversion Principle (DIP)**

---

## ✅ La Solución: Refactorización

### Estrategia de Refactorización

Para resolver estos problemas, aplicamos:
1. **Convertir la clase estática a una clase instanciable** → Permite crear instancias
2. **Hacer el método virtual** → Permite que sea sobrescrito (mockeable)
3. **Crear una variable privada en el servicio** → Para inyectar la dependencia
4. **Instanciar en el constructor** → Para inicializar la dependencia
5. **Renombrar significativamente** → `PingPongHelper2` → `PingPongWinRepository2` (especifica la funcionalidad)

### Opción 1: Solución Simple (Sin Interfaz)

#### `PingPongWinRepository2.cs` (Nueva clase instanciable)

```csharp
// CAMBIOS:
// ✅ Cambiar de 'public static class' a 'public class'
// ✅ El método GetWin() ahora es 'virtual' para poder mockearlo/sobrescribirlo

public class PingPongWinRepository2
{
    public virtual string GetWin()
    {
        var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
        return File.ReadAllText(filePath);
    }
}
```

#### `PingPongService2.cs` (Refactorizado)

```csharp
public class PingPongService2
{
    private const string Fault = "Fault";
    private const string Ping = "Ping";
    private const string PingPong = "PingPong";
    private const string Pong = "Pong";

    // ✅ CAMBIO 1: Variable privada para la dependencia
    private readonly PingPongWinRepository2 _winRepository;

    // ✅ CAMBIO 2: Constructor que inyecta la dependencia
    public PingPongService2() 
        : this(new PingPongWinRepository2())
    {
    }

    // ✅ CAMBIO 3: Constructor alternativo para testing (permite inyectar mock)
    public PingPongService2(PingPongWinRepository2 winRepository)
    {
        _winRepository = winRepository ?? throw new ArgumentNullException(nameof(winRepository));
    }

    public string GetPingPongGame(string action)
    {
        if (string.IsNullOrEmpty(action))
        {
            throw new ArgumentNullException(nameof(action));
        }

        if (action == Ping)
        {
            return action + Pong;
        }

        if (action == Pong)
        {
            return Ping + action;
        }

        if (action == PingPong)
        {
            // ✅ CAMBIO 4: Usar la instancia inyectada en lugar del método estático
            return action + _winRepository.GetWin();
        }

        return Fault;
    }
}
```

### Opción 2: Solución Profesional (Con Interfaz) - Recomendada

Esta es la mejor práctica para aplicar **Dependency Inversion Principle (DIP)**:

#### `IPingPongWinRepository.cs` (Nueva interfaz)

```csharp
public interface IPingPongWinRepository
{
    string GetWin();
}
```

#### `PingPongWinRepository2.cs` (Implementa la interfaz)

```csharp
public class PingPongWinRepository2 : IPingPongWinRepository
{
    public string GetWin()
    {
        var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
        return File.ReadAllText(filePath);
    }
}
```

#### `PingPongService2.cs` (Depende de la interfaz)

```csharp
public class PingPongService2
{
    private const string Fault = "Fault";
    private const string Ping = "Ping";
    private const string PingPong = "PingPong";
    private const string Pong = "Pong";

    // ✅ Depende de la interfaz, no de la implementación
    private readonly IPingPongWinRepository _winRepository;

    public PingPongService2() 
        : this(new PingPongWinRepository2())
    {
    }

    public PingPongService2(IPingPongWinRepository winRepository)
    {
        _winRepository = winRepository ?? throw new ArgumentNullException(nameof(winRepository));
    }

    public string GetPingPongGame(string action)
    {
        if (string.IsNullOrEmpty(action))
        {
            throw new ArgumentNullException(nameof(action));
        }

        if (action == Ping)
        {
            return action + Pong;
        }

        if (action == Pong)
        {
            return Ping + action;
        }

        if (action == PingPong)
        {
            return action + _winRepository.GetWin();
        }

        return Fault;
    }
}
```

---

## 🧪 Ventajas para Testing

### Antes (Imposible Mockear)

```csharp
[Test]
public void PingPongService_GetPingPongGame_When_GetWin_returns_custom_value()
{
    // ❌ IMPOSIBLE: No podemos mockear PingPongHelper2.GetWin()
    // Los tests siempre ejecutan el código real, leyendo archivos reales
    var service = new PingPongService2();
    var result = service.GetPingPongGame("PingPong");
    // Depende del archivo "win.txt" existente
}
```

### Después (Fácil de Mockear)

```csharp
[Test]
public void PingPongService_GetPingPongGame_When_GetWin_returns_custom_value()
{
    // ✅ POSIBLE: Creamos un mock de la interfaz
    var mockWinRepository = new Mock<IPingPongWinRepository>();
    mockWinRepository.Setup(x => x.GetWin()).Returns("MockWin");
    
    var service = new PingPongService2(mockWinRepository.Object);
    var result = service.GetPingPongGame("PingPong");
    
    Assert.That(result, Is.EqualTo("PingPongMockWin"));
}
```

---

## 📊 Comparación Resumen

| Aspecto | Antes (Estático) | Después (Inyección) |
|--------|------------------|-------------------|
| **Instanciable** | ❌ No | ✅ Sí |
| **Mockeable** | ❌ No | ✅ Sí |
| **Testeable** | ❌ No (acoplado) | ✅ Sí (desacoplado) |
| **Extensible** | ❌ No | ✅ Sí |
| **Mantenible** | ❌ No | ✅ Sí |
| **Inyectable** | ❌ No | ✅ Sí |
| **SOLID** | ❌ No (viola DIP) | ✅ Sí (cumple DIP) |

---

## 🎯 Conceptos Clave Aplicados

### 1. **Dependency Inversion Principle (DIP)**
- **Antes**: `PingPongService2` dependía directamente de `PingPongHelper2`
- **Después**: `PingPongService2` depende de `IPingPongWinRepository` (abstracción)

### 2. **Inyección de Dependencias**
- Las dependencias se pasan al constructor en lugar de ser creadas internamente
- Permite usar diferentes implementaciones según el contexto (producción vs. testing)

### 3. **Polimorfismo**
- Podemos tener múltiples implementaciones de `IPingPongWinRepository`
- Cada una puede ser mockeable para diferentes escenarios de testing

### 4. **Separación de Responsabilidades**
- `PingPongService2`: Lógica del juego
- `PingPongWinRepository2`: Acceso a datos del ganador
- Cada clase tiene una única responsabilidad

---

## 🔧 Puntos Importantes en la Refactorización

### ❌ NO hagas esto:

```csharp
// ❌ MALO: Llamar al constructor sin parámetros crea una nueva instancia cada vez
public string GetPingPongGame(string action)
{
    var winRepository = new PingPongWinRepository2(); // Acoplamiento implícito
    return action + winRepository.GetWin();
}
```

### ✅ HAZ esto:

```csharp
// ✅ CORRECTO: Usar la instancia inyectada en el constructor
private readonly IPingPongWinRepository _winRepository;

public PingPongService2(IPingPongWinRepository winRepository)
{
    _winRepository = winRepository ?? throw new ArgumentNullException(nameof(winRepository));
}

public string GetPingPongGame(string action)
{
    return action + _winRepository.GetWin();
}
```

---

## 📚 Recursos Adicionales

### Principios SOLID aplicados:
- **D** - Dependency Inversion: Depender de abstracciones, no de implementaciones concretas
- **S** - Single Responsibility: Cada clase tiene una única razón para cambiar
- **O** - Open/Closed: Abierto para extensión, cerrado para modificación

### Patrones utilizados:
- **Dependency Injection**: Inyectar dependencias a través del constructor
- **Strategy Pattern**: Diferentes estrategias para obtener el ganador
- **Template Method**: La clase base define la estructura, la subclase los detalles

---

## ✅ Checklist de Validación

Al completar este ejercicio, verifica:

- [ ] La clase estática ha sido convertida a una clase instanciable
- [ ] El método `GetWin()` es `virtual` o está en una interfaz
- [ ] `PingPongService2` tiene una variable privada readonly para la dependencia
- [ ] Existe un constructor que acepta la dependencia
- [ ] El nombre de la clase refleja su funcionalidad (`PingPongWinRepository2`, no `Helper`)
- [ ] Los tests pueden inyectar un mock de la dependencia
- [ ] Los tests pueden verificar el comportamiento sin acceder a archivos reales
- [ ] Todos los tests pasan correctamente

---

## 🎓 Conclusión

Esta refactorización transforma código fuertemente acoplado y no testeable en código flexible, mantenible y profesional. El Dependency Inversion Principle es fundamental para escribir código de calidad que sea fácil de probar y mantener.

**Recuerda**: Un buen código no solo funciona hoy, sino que facilita cambios y pruebas en el futuro.
