# Skyline.DataMiner.Utils.AutoMethodMapper

## Table of Content

- [About](#About-DataMiner)
- [Installation](#Installation)
- [Usage/Examples](#UsageExamples)
    - [Map by integer](#Map-By-Integer)
    - [Map by string](#Map-By-String)
    - [Custom mapper](#Custom-Mapper)

## About

Have you ever found yourself working on a programming project where you had a lot of triggers to manage for a QAction? It can quickly become overwhelming and complicated to keep track of all the different actions that need to be performed. But don't worry, there's a solution that can make your life easier!

By using this nuget, you can automate the mapping of triggers to specific actions using a technique called reflection. This means you don't have to manually write out a bunch of code and create a large switch statement. Instead, you can let the nuget do the work for you and keep your code organized and easy to maintain.

By splitting up the functionality into different files, you can keep your codebase clean and make it easier for others to understand and work with. So don't let a complex QAction hold you back - take advantage of this powerful tool and simplify your coding experience today! For example mapping a trigger for a device reboot button to a function that sends the request to the device can be: 
```csharp
[MapperInt(Parameter.Servers.Pid.Write.serversreboot_1209)]
public void SendRequestForDeviceReboot(SLProtocolExt protocol)
{
    var rowKey = protocol.RowKey();
    protocol.Postserverrebooturl_110 = "api/content_processing/reboot/" + rowKey;

    // Check the trigger that starts the http get call
    protocol.CheckTrigger((int)CheckTrigger.RebootServer);
}
```

### About DataMiner

DataMiner is a transformational platform that provides vendor-independent control and monitoring of devices and services. Out of the box and by design, it addresses key challenges such as security, complexity, multi-cloud, and much more. It has a pronounced open architecture and powerful capabilities enabling users to evolve easily and continuously.

The foundation of DataMiner is its powerful and versatile data acquisition and control layer. With DataMiner, there are no restrictions to what data users can access. Data sources may reside on premises, in the cloud, or in a hybrid setup.

A unique catalog of 7000+ connectors already exist. In addition, you can leverage DataMiner Development Packages to build you own connectors (also known as "protocols" or "drivers").

> **Note**
> See also: [About DataMiner](https://aka.dataminer.services/about-dataminer).

### About Skyline Communications

At Skyline Communications, we deal in world-class solutions that are deployed by leading companies around the globe. Check out [our proven track record](https://aka.dataminer.services/about-skyline) and see how we make our customers' lives easier by empowering them to take their operations to the next level.



## Installation

Add the nuget package to your QAction with the "Manage Nuget Packges..."

## Usage/Examples

### Map By Integer

If your mapping uses integer values for example parameter id's, you can use the MethodMapperByInt class. Create a class (or partial class if you want to split it up in seperate files) and inherit from the MethodMapperByInt class.

```csharp
using Skyline.DataMiner.Utils.AutoMethodMapper;

public class HandleSets : MethodMapperByInt 
{
    [MapperInt(Parameter.TriggerOne)]
    public void HandleSetOne(SLProtocol protocol)
    {
        // Do thing one
    }

    [MapperInt(Parameter.TriggerTwo)]
    public void HandleSetTwo(SLProtocol protocol)
    {
        // Do thing two
    }

    [MapperInt(Parameter.TriggerThree)]
    public void HandleSetThree(SLProtocol protocol)
    {
        // Do thing three
    }

    ...

    [MapperInt(Parameter.TriggerTwenty)]
    public void HandleSetTwenty(SLProtocol protocol)
    {
        // Do thing twenty
    }
}
```

In the QAction you need to instantiate a new object from the newly created class and call the Process method.

```csharp 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            var setHandler = new HandleSets(protocol.GetTriggerParameter());
            setHandler.Process(protocol);
        }
        catch (Exception ex)
        {
            protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
        }
    }
}
```

### Map By String

If your mapping uses string values for example parameter id's, you can use the MethodMapperByString class. Create a class (or partial class if you want to split it up in seperate files) and inherit from the MethodMapperByString class.

```csharp
using Skyline.DataMiner.Utils.AutoMethodMapper;

public class HandleRowSets : MethodMapperByString
{
    [MapperString("RowOne")]
    public void HandleSetOne(SLProtocol protocol)
    {
        // Do thing one
    }

    [MapperString("RowTwo")]
    public void HandleSetTwo(SLProtocol protocol)
    {
        // Do thing two
    }

    [MapperString("RowThree")]
    public void HandleSetThree(SLProtocol protocol)
    {
        // Do thing three
    }

    ...

    [MapperString("RowTwenty")]
    public void HandleSetTwenty(SLProtocol protocol)
    {
        // Do thing twenty
    }
}
```

In the QAction you need to instantiate a new object from the newly created class and call the Process method.

```csharp 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            var setHandler = new HandleRowSets(protocol.RowKey());
            setHandler.Process(protocol);
        }
        catch (Exception ex)
        {
            protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
        }
    }
}
```

### Custom Mapper

If there is another type you want to use as a key for the mapping then you can create a custom attribute for this. You can then inherit from the basic MethodMapper class and define your custom attribute there so the mapper knows what to look for. 

Let's say we have a class called "Custom".

```csharp
public class Custom
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Custom(string firstName, string lastName) 
    {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
}
```

Then we will need an attribute that stores an instance of this class

```csharp
using System;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class MapperCustomAttribute : Attribute
{
    public MapperCustomAttribute(Custom key)
    {
        this.CustomKey = key;
    }

    public Custom CustomKey { get; }
}
```

Now we will create or custom mapper that inherits from the "MethodMapper" class. The base class needs the type of the custom attribute, so it knows what to look for. We will create or own Process method that searches through the mapping and invokes the correct method, with the given arguments.

```csharp
using Skyline.DataMiner.Utils.AutoMethodMapper;

public partial class HandleCustomSets : MethodMapper 
{
    public HandleCustomSets() : base(typeof(MapperCustomAttribute)) {}

    public HandleCustomSets(Custom trigger) : base(typeof(MapperCustomAttribute)) 
    {
        this.Trigger = trigger;
    }

    public Custom Trigger { get; set; }

    public override void Process(params object[] args) 
    {
        this.Process(this.Trigger, args);
    }

    public void Process(Custom trigger, params object[] args) 
    {
        try 
        {
            var method = this.Mapping.FirstOrDefault(pair => ((MapperCustomAttribute)pair.Key).CustomKey.FirstName == trigger.FirstName && ((MapperCustomAttribute)pair.Key).CustomKey.LastName == trigger.LastName).Value;
            method.Invoke(this, args);
        }
        catch (Exception ex) 
        {
            throw new TriggerNotSupportedException(ex);
        }
    }
}
```

You can then make a new class that inherits from your custom mapper class, just like we did in the map by int and map by string examples above. Or you can make the custom mapper class partial and just write the methods in an other file or just directly in the same one.

```csharp
using Skyline.DataMiner.Utils.AutoMethodMapper;

public partial class HandleCustomSets
{
    [MapperCustom(new Custom("firstname_one", "lastname_one"))]
    public void HandleSetOne(SLProtocol protocol)
    {
        // Do thing one
    }

    [MapperCustom(new Custom("firstname_two", "lastname_two"))]
    public void HandleSetTwo(SLProtocol protocol)
    {
        // Do thing two
    }

    [MapperCustom(new Custom("firstname_three", "lastname_three"))]
    public void HandleSetThree(SLProtocol protocol)
    {
        // Do thing three
    }

    ...

    [MapperCustom(new Custom("firstname_twenty", "lastname_twenty"))]
    public void HandleSetTwenty(SLProtocol protocol)
    {
        // Do thing twenty
    }
}
```

In the QAction you need to instantiate a new object from the newly created class and call the Process method.

```csharp 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocol protocol)
    {
        try
        {
            var names = (object[])protocol.GetParameters(Parameter.firstname, Parameter.lastname);
            var customObject = new Custom 
            {
                FirstName = Convert.ToString(names[0]),
                LastName = Convert.ToString(names[1]),
            };
            var setHandler = new HandleCustomSets(customObject);
            setHandler.Process(protocol);
        }
        catch (Exception ex)
        {
            protocol.Log("QA" + protocol.QActionID + "|" + protocol.GetTriggerParameter() + "|Run|Exception thrown:" + Environment.NewLine + ex, LogType.Error, LogLevel.NoLogging);
        }
    }
}
```