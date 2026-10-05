# Ejercicio 3: Interface Segregation Principle (ISP)

## 📋 Descripción General

En este ejercicio, aplicaremos el **Interface Segregation Principle (ISP)** del acrónimo SOLID. Este principio establece que:

> **"Los clientes no deberían depender de interfaces que no utilizan"**

El objetivo es refactorizar el código de la solución base para crear un contrato de interfaz que desacople el servicio de la implementación concreta de la clase `PingPongWinRepository3`.

---

## 🎯 Objetivo

Transformar la dependencia a una clase concreta (`PingPongWinRepository3`) en una dependencia a una interfaz segregada que expone únicamente el comportamiento necesario.

---

## 🔍 Análisis del Problema

### Situación Actual (BaseSolution)

La clase `PingPongService3` está fuertemente acoplada a la clase concreta `PingPongWinRepository3`:

```csharp
public class PingPongService3
{
    private readonly PingPongWinRepository3 pingPongWinRepository;

    public PingPongService3()
    {
        this.pingPongWinRepository = new PingPongWinRepository3();
    }

    public string GetPingPongGame(string action)
    {
        // ...
        if (action == PingPong)
        {
            return action + this.pingPongWinRepository.GetWin();
        }
        // ...
    }
}
```

### Problemas Identificados

1. **Acoplamiento fuerte**: El servicio depende de una clase concreta en lugar de una abstracción.
2. **Dificultad para testing**: Es complejo crear mocks o stubs de `PingPongWinRepository3` porque la clase no implementa una interfaz.
3. **Falta de segregación**: El cliente (`PingPongService3`) se ve obligado a "conocer" toda la clase `PingPongWinRepository3`, aunque solo necesita el método `GetWin()`.
4. **Violación del Interface Segregation Principle**: Estamos exponiendo un contrato más complejo de lo necesario.

---

## 💡 Preguntas Clave para Resolver el Ejercicio

Antes de implementar, reflexiona sobre:

1. **¿Qué formas hay de exponer contratos a través de las clases?**
   - Las propias definiciones de las clases
   - Las clases abstractas
   - Las **interfaces** ✨

2. **¿Cuál es la diferencia entre una clase abstracta y una interfaz?**
   - Las clases abstractas pueden tener comportamiento genérico y más dependencias
   - Las interfaces definen un contrato puro y segregado
   - Las interfaces permiten el mínimo acoplamiento entre dependencias

3. **¿Qué deberíamos hacer en este caso?**
   - Extraer una interfaz de `PingPongWinRepository3`
   - Crear una interfaz que represente el contrato: "obtener un resultado de victoria"
   - Inyectar la interfaz en lugar de la clase concreta

4. **¿Debería la interfaz llamarse `IPingPongWinRepository3` o algo más segregado?**
   - Considera un nombre más descriptivo que refleje el comportamiento específico
   - Ejemplo: `IWinProvider`, `IWinRepository`, etc.

---

## 🛠️ Tareas a Realizar

### 1️⃣ Crear la Interfaz

Crea una nueva interfaz que defina el contrato necesario para obtener un "Win":

```csharp
public interface IPingPongWinRepository3
{
    string GetWin();
}
```

**Ubicación**: `.codeSamples/03-SOLID/Exercises/CompleteSolution/Exercise3/IPingPongWinRepository3.cs`

### 2️⃣ Implementar la Interfaz en PingPongWinRepository3

Modifica la clase `PingPongWinRepository3` para que implemente la interfaz:

```csharp
public class PingPongWinRepository3 : IPingPongWinRepository3
{
    public virtual string GetWin()
    {
        var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
        return File.ReadAllText(filePath);
    }
}
```

### 3️⃣ Refactorizar PingPongService3

Modifica la clase `PingPongService3` para:
- Cambiar el tipo de la dependencia de `PingPongWinRepository3` a `IPingPongWinRepository3`
- Aceptar la interfaz en el constructor para permitir inyección de dependencias
- Mantener la funcionalidad original intacta

```csharp
public class PingPongService3
{
    private readonly IPingPongWinRepository3 winRepository;

    public PingPongService3() : this(new PingPongWinRepository3())
    {
    }

    public PingPongService3(IPingPongWinRepository3 winRepository)
    {
        this.winRepository = winRepository;
    }

    public string GetPingPongGame(string action)
    {
        // ... código existente, usando this.winRepository en lugar de this.pingPongWinRepository
    }
}
```

---

## ✅ Beneficios de Esta Refactorización

### 1. **Menor Acoplamiento**
   - El servicio ahora depende de una abstracción, no de una implementación concreta

### 2. **Mejora de Testabilidad**
   - Ahora podemos crear fácilmente mocks o stubs de `IPingPongWinRepository3`
   - Los tests unitarios pueden inyectar implementaciones falsas

   ```csharp
   [Test]
   public void PingPongService3_GetPingPongGame_WithValidAction_ReturnsExpectedResult()
   {
       // Arrange
       var mockRepository = new Mock<IPingPongWinRepository3>();
       mockRepository.Setup(r => r.GetWin()).Returns("Win");
       
       var service = new PingPongService3(mockRepository.Object);

       // Act
       var result = service.GetPingPongGame("PingPong");

       // Assert
       Assert.That(result, Is.EqualTo("PingPongWin"));
   }
   ```

### 3. **Segregación de Interfaces**
   - Exponemos solo el contrato necesario: `GetWin()`
   - El cliente no necesita "conocer" detalles internos de la clase

### 4. **Flexibilidad**
   - Podemos cambiar fácilmente la implementación de `IPingPongWinRepository3`
   - Podemos tener múltiples implementaciones sin afectar al servicio

### 5. **Cumplimiento de SOLID**
   - Respetamos el **Interface Segregation Principle**
   - Mantenemos el **Open/Closed Principle** (abierto a extensión, cerrado a modificación)
   - Seguimos el **Dependency Inversion Principle** (depender de abstracciones, no de concreciones)

---

## 📝 Checklist de Implementación

- [ ] Creé la interfaz `IPingPongWinRepository3` con el método `GetWin()`
- [ ] La clase `PingPongWinRepository3` implementa la interfaz
- [ ] Cambié el tipo de la dependencia en `PingPongService3` a `IPingPongWinRepository3`
- [ ] Agregué un constructor que acepte la interfaz como parámetro
- [ ] Mantuve un constructor sin parámetros que crea una instancia por defecto (para compatibilidad)
- [ ] Todos los tests unitarios siguen pasando
- [ ] El comportamiento de la aplicación es el mismo que en la solución base

---

## 🧪 Validación

Para validar tu implementación:

1. **Compila el código**: Asegúrate de que no hay errores de compilación
2. **Ejecuta los tests existentes**: Todos los tests deben pasar sin cambios
3. **Verifica el acoplamiento**: Revisa que `PingPongService3` solo dependa de `IPingPongWinRepository3`
4. **Prueba con mocks**: Intenta crear un test con un mock de la interfaz para verificar que funciona correctamente

---

## 🔗 Conceptos Relacionados

- **SOLID Principles**: Interface Segregation Principle
- **Dependency Injection**: Inyección de dependencias a través del constructor
- **Mocking**: Usar mocks en lugar de clases concretas para testing
- **Abstraction**: Depender de abstracciones en lugar de concreciones

---

## 📚 Recursos Adicionales

- Capítulo sobre Principios SOLID en "The Art of Unit Testing"
- Documentación de interfaces en C#
- Patrones de diseño: Strategy, Repository, etc.

---

## 🎓 Conclusión

El **Interface Segregation Principle** nos enseña que las interfaces deben ser **específicas y segregadas** para cada cliente. Al refactorizar este código, hemos conseguido un diseño más flexible, testeable y mantenible que sigue los principios SOLID.

El cambio es pequeño pero poderoso: de depender de una clase concreta a depender de un contrato (interfaz) que define exactamente lo que necesitamos.

