# 🎯 Ejercicio: Dependency Inversion Principle (DIP)

## 📌 Objetivo

En este ejercicio aprenderás a implementar correctamente el **Dependency Inversion Principle** (DIP), uno de los 5 principios SOLID. Al finalizar, comprenderás por qué el DIP mejora la testabilidad, mantenibilidad y flexibilidad del código.

---

## 📚 Conceptos Teóricos

### ¿Qué es el Dependency Inversion Principle?

El DIP establece dos principios fundamentales:

1. **Las clases de alto nivel no deben depender de clases de bajo nivel. Ambas deben depender de abstracciones.**
2. **Las abstracciones no deben depender de detalles. Los detalles deben depender de abstracciones.**

### Ejemplo de Violación del DIP

```csharp
public class PingPongService
{
    public string GetPingPongGame(string action)
    {
        // ❌ PROBLEMA: Dependencia directa de una clase concreta
        var win = PingPongHelper.GetWin("win.txt");  // Acoplamiento fuerte
        return action + win;
    }
}
```

**Problemas:**
- ❌ No es testeable (no puedes mockear `PingPongHelper`)
- ❌ No es mantenible (cambios en `PingPongHelper` afectan al servicio)
- ❌ No es extensible (no puedes cambiar la implementación fácilmente)

### Ejemplo Correcto de DIP

```csharp
public class PingPongService
{
    private readonly IWinRepository _winRepository;  // ✅ Depende de abstracción

    public PingPongService(IWinRepository winRepository)
    {
        _winRepository = winRepository;
    }

    public string GetPingPongGame(string action)
    {
        // ✅ SOLUCIÓN: Usa la abstracción inyectada
        var win = _winRepository.GetWin();
        return action + win;
    }
}
```

**Beneficios:**
- ✅ Es testeable (puedes crear un mock de `IWinRepository`)
- ✅ Es mantenible (cambios en la implementación no afectan al servicio)
- ✅ Es extensible (puedes usar diferentes implementaciones de `IWinRepository`)

---

## 🔍 Análisis del Código Actual

### Antes (Violación de DIP)

En el archivo original `PingPongService4.cs`, encontrábamos:

```csharp
public class PingPongService4
{
    public string GetPingPongGame(string action)
    {
        if (action == "PingPong")
        {
            // ❌ Acoplamiento directo a ConfigurationManager y PingPongHelper
            var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
            return action + PingPongHelper4.GetWin(filePath);
        }
        return "Fault";
    }
}
```

**Problemas identificados:**
1. Dependencia directa de `ConfigurationManager` (infraestructura)
2. Dependencia directa de `PingPongHelper4` (clase estática)
3. No testeable sin efectos secundarios en la configuración
4. Altamente acoplado y difícil de mantener

### Después (Implementación Correcta de DIP)

Ahora el código implementa DIP correctamente:

```csharp
public class PingPongService4
{
    private readonly IWinRepository _winRepository;  // ✅ Campo privado

    public PingPongService4(IWinRepository winRepository)  // ✅ Inyección en constructor
    {
        _winRepository = winRepository ?? throw new ArgumentNullException(nameof(winRepository));
    }

    public string GetPingPongGame(string action)
    {
        if (action == "PingPong")
        {
            // ✅ Usa la abstracción inyectada
            return action + _winRepository.GetWin();
        }
        return "Fault";
    }
}
```

**Mejoras:**
- ✅ Depende de una interfaz (`IWinRepository`), no de clases concretas
- ✅ La dependencia se inyecta en el constructor
- ✅ Validación nula del parámetro de dependencia
- ✅ Totalmente testeable con mocks

---

## 🧪 Verificación con Tests Unitarios

### Test 1: Inyección correcta de dependencia

```csharp
[Test]
public void PingPongService4_Ctor_with_null_dependencies()
{
    // El constructor valida que la dependencia no sea nula
    Assert.Throws<ArgumentNullException>(() => new PingPongService4(null));
}
```

**¿Qué prueba?** Que el principio de DIP se respeta validando que la abstracción no sea nula.

### Test 2: Comportamiento con diferentes implementaciones

```csharp
[TestCase("Ping", "PingPong")]
[TestCase("Pong", "PingPong")]
[TestCase("PingPong", "PingPongWin")]
[TestCase("AAAAA", "Fault")]
public void PingPongService4_GetPingPongGame_When_getting_with_action_then_gets_action_and_win(
    string action,
    string actionResult)
{
    // Arrange: Se inyecta una implementación concreta de IWinRepository
    var pingPongWinRepositoryDependency = new PingPongWinRepository4();
    var service = new PingPongService4(pingPongWinRepositoryDependency);

    // Act
    var result = service.GetPingPongGame(action);

    // Assert
    Assert.That(result, Is.EqualTo(actionResult));
}
```

**¿Qué prueba?** Que el servicio funciona correctamente cuando se inyecta una implementación de `IWinRepository`.

### Test 3: Cuando la acción es nula

```csharp
[Test]
public void PingPongService4_GetPingPongGame_When_action_is_null_then_throws_error()
{
    var pingPongWinRepositoryDependency = new PingPongWinRepository4();
    Assert.Throws<ArgumentNullException>(() => 
        new PingPongService4(pingPongWinRepositoryDependency).GetPingPongGame(null));
}
```

**¿Qué prueba?** Que el servicio valida la entrada del usuario.

---

## 📝 Pasos para Completar el Ejercicio

### Paso 1: Crear una Interfaz (IWinRepository)

**Ubicación:** `TheArtOfUnitTesting.Services/IWinRepository.cs`

Ya existe, pero verifícalo:

```csharp
public interface IWinRepository
{
    string GetWin();
}
```

✅ **Objetivo:** Definir el contrato que todo repositorio debe cumplir.

---

### Paso 2: Implementar la Interfaz (PingPongWinRepository4)

**Ubicación:** `TheArtOfUnitTesting.Services/PingPongWinRepository4.cs`

Verifica que implemente `IWinRepository`:

```csharp
public class PingPongWinRepository4 : IWinRepository
{
    public virtual string GetWin()
    {
        var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
        return File.ReadAllText(filePath);
    }
}
```

✅ **Objetivo:** Proporcionar una implementación concreta de la interfaz.

---

### Paso 3: Refactorizar el Servicio (PingPongService4)

**Ubicación:** `TheArtOfUnitTesting.Services/PingPongService4.cs`

El servicio debe:

1. **Tener un campo privado de la interfaz:**
```csharp
private readonly IWinRepository _winRepository;
```

2. **Recibir la dependencia en el constructor:**
```csharp
public PingPongService4(IWinRepository winRepository)
{
    _winRepository = winRepository ?? throw new ArgumentNullException(nameof(winRepository));
}
```

3. **Usar la abstracción inyectada:**
```csharp
if (action == PingPong)
{
    return action + _winRepository.GetWin();
}
```

✅ **Objetivo:** Invertir la dependencia de una clase concreta a una abstracción.

---

### Paso 4: Escribir Tests (PingPongService4Tests)

**Ubicación:** `TheArtOfUnitTesting.Services.UnitTests/PingPongService4Tests.cs`

Ya existen los tests, pero verifícalos:

```csharp
[Test]
public void PingPongService4_Ctor_with_null_dependencies()
{
    Assert.Throws<ArgumentNullException>(() => new PingPongService4(null));
}

[TestCase("Ping", "PingPong")]
[TestCase("Pong", "PingPong")]
[TestCase("PingPong", "PingPongWin")]
[TestCase("AAAAA", "Fault")]
public void PingPongService4_GetPingPongGame_When_getting_with_action_then_gets_action_and_win(
    string action,
    string actionResult)
{
    var repository = new PingPongWinRepository4();
    var service = new PingPongService4(repository);
    var result = service.GetPingPongGame(action);
    Assert.That(result, Is.EqualTo(actionResult));
}
```

✅ **Objetivo:** Verificar que el servicio funciona correctamente con la inyección de dependencias.

---

### Paso 5: Crear un Mock para Testeo (Desafío Adicional)

Para demostrar el poder del DIP, crea un mock de `IWinRepository`:

```csharp
// En tus tests, crea una clase helper:
public class MockWinRepository : IWinRepository
{
    public string GetWin()
    {
        return "MockWin";  // Retorna un valor predecible
    }
}

// Úsalo en un test:
[Test]
public void PingPongService4_GetPingPongGame_With_mock_repository()
{
    // Arrange
    var mockRepository = new MockWinRepository();
    var service = new PingPongService4(mockRepository);

    // Act
    var result = service.GetPingPongGame("PingPong");

    // Assert
    Assert.That(result, Is.EqualTo("PingPongMockWin"));
}
```

✅ **Objetivo:** Demostrar que con DIP puedes reemplazar fácilmente la implementación.

---

## 🎓 Preguntas de Reflexión

1. **¿Qué sucedería si no tuviéramos `IWinRepository`?**
   - Respuesta: El servicio estaría acoplado a `PingPongWinRepository4` específicamente.

2. **¿Por qué el constructor valida la dependencia?**
   - Respuesta: Para asegurar que el servicio nunca funcione sin una implementación válida.

3. **¿Cómo te ayuda DIP a escribir mejores tests?**
   - Respuesta: Puedes crear mocks o stubs de `IWinRepository` sin efectos secundarios.

4. **¿Es posible cambiar la implementación en runtime?**
   - Respuesta: Sí, inyectando diferentes implementaciones de `IWinRepository`.

---

## ✅ Checklist de Validación

Verifica que tu implementación cumple con:

- [ ] `PingPongService4` tiene un campo privado `IWinRepository`
- [ ] El constructor recibe `IWinRepository` como parámetro
- [ ] El constructor valida que el parámetro no sea nulo
- [ ] El método `GetPingPongGame` usa `_winRepository.GetWin()` en lugar de clases estáticas
- [ ] `PingPongWinRepository4` implementa `IWinRepository`
- [ ] Todos los tests pasan ✅
- [ ] El código es testeable con mocks ✅
- [ ] El código es mantenible y extensible ✅

---

## 🚀 Próximos Pasos

Ahora que comprendes DIP, puedes:

1. **Aplicar DIP en otros servicios:** Refactoriza otros servicios siguiendo el mismo patrón.
2. **Usar un contenedor de DI:** Explora `Microsoft.Extensions.DependencyInjection` o `Autofac`.
3. **Combinar con otros principios SOLID:** DIP funciona mejor cuando se combina con SRP, OCP, etc.
4. **Explorar patrones avanzados:** Factory Pattern, Service Locator, etc.

---

## 📖 Referencias

- **Libro:** "The Art of Unit Testing" - Roy Osherove
- **Principios SOLID:** https://en.wikipedia.org/wiki/SOLID
- **Dependency Injection:** https://martinfowler.com/articles/injection.html
- **NUnit Docs:** https://docs.nunit.org/

---

## 💡 Conclusión

El **Dependency Inversion Principle** es esencial para escribir código profesional, testeable y mantenible. Al invertir las dependencias (dependiendo de abstracciones en lugar de implementaciones concretas), tu código se vuelve:

- ✅ **Más testeable:** Puedes mockear fácilmente las dependencias
- ✅ **Más mantenible:** Los cambios en una implementación no afectan al código que la usa
- ✅ **Más extensible:** Puedes agregar nuevas implementaciones sin modificar código existente
- ✅ **Más flexible:** Puedes cambiar implementaciones en runtime

¡Ahora estás listo para aplicar DIP en tus proyectos! 🎉
