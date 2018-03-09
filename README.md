# Consolification

Consolification is a little framework to help you to write C# Console Application, by managing argument parsing, help text generation and much more.

#### The main features are:

- Automatically set class property values from given Console Application argument values (by associating Property and argument names via dedicated attributes). 
So, you don't have to parse given application arguments yourself.
- Manages mandatory arguments and complex application argument structure, with parents and children concept: A child argument is an argument that is relevant only if another 'parent' argument is also specified in the Console Application command line. You can have several level of children arguments. Consolification manages that for you, by refusing, for example, a given child argument if a parent is not specified etc. 
- Automatically generate help text by using argument description provided within dedicated attributes.
- Encapsulate your code logic within a dedicated Job class, so you do not have to take care about Exception handling, help display (for example in case of wrong argument specified).


#### Example for a quick start:

For a quick example, we will simply create a small Console Application that just display the text given in an argument.
As soon as you have create the Console Application (with a minimum .NET 4.6.2 version), add a dedicated class to handle data:
 
```C#
[CIHelpArgument("/?")]
[CIJob(typeof(MessageJob))]
public class Data
{
  [CISimpleArgument(0, "message", "The message to display.")]
  public string Message { get; private set; }
}
```

The first attribute `CIHelpArgument`, on the class `Data`, specifies that you need to provide the argument '/?' to view the corresponding help.
The second class `Data` attribute `CIJob` indicates that the application logic is implemented within the class `MessageJob`.
The attribute `CISimpleArgument` on the property `Message` indicates that the Message property will receive the value of the first argument provided to the application (index 0). The other attribute parameters are dedicated to the automatically generated help text. We'll see that later.

Then, simply modify the Program class with the Main method like that:

```C#
class Program
{
  static int Main(string[] args)
  {
    ConsolificationEngine<Data> engine = new ConsolificationEngine<Data>();
    return engine.Start(args);
  }
}
```


Now, you just have to execute your application by giving a message as the single available argument (except the /? argument defined via the `CIHelpArgument` attribute.

For example, if your Console Application executable  is named Example.exe, juste type in the console:

```Batchfile
> Example "Hello the world!"
```

The result output will be simply 
```Batchfile
Hello the world!
```

Now, if you type: `> Example /?` the output will be the automatically generated help text:

```Batchfile
Usage: Example [/?] [message]

message The message to display.
```

The brackets [] indicates optional arguments. If you specify that Message is a mandatory argument, by using the CIMandatoryArgumentAttribute, the help displayed will be:

```Batchfile
Usage: Example [/?] message

message The message to display.
```

The word 'message' to name the related argument has been specified in the CISimpleArgumentAttribute as second parameter. And the corresponding description ('The message to display') is taken from the last parameter of this same attribute.

Of course, for a so simple example, Consolification is not really usefull. But wait and see what will be the benefits when things are being more complex.


#### Consolification Attribute list:

- `CISimpleArgumentAttribute`: The attribute used in the example above.

- `CINamedArgumentAttribute`: Use this attribute to define an argument that has got a specific name. Imagines for example you have a Console Application for which you can pass two arguments like `/URL http://www.google.fr`. In this case, use the `CINamesdArgumentAttribute` like that in your corresponding data class:

```C#
 [CINamedArgument("/URL", "value", "The URL of the request to perform.")]
 [CIMandatoryArgument]
 public Uri URL { get; private set; }
```
