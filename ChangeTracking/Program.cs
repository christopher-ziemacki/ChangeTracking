using System.Reflection;
using ChangeTracking.Models;

var person = new Person("Gregory", "Wisneski");

var personType = typeof(Person);

var properties = personType.GetProperties();

var snapshots = properties.Select(p => new PropertySnapshot(p, person)).ToArray();

person.FirstName = "John";

foreach (var snapshot in snapshots)
{
    var currentValue = snapshot.Property.GetValue(person);
    if (currentValue != snapshot.Value)
    {
        Console.WriteLine($"'{snapshot.Property.Name}' property value has changed." +
                          $" Previous value was '{snapshot.Value}'." +
                          $" Current value is '{currentValue}'.");
    }
}

class PropertySnapshot
{
    public PropertyInfo Property { get; }
    public object Target { get; }
    public object Value { get; }
 
    public PropertySnapshot(PropertyInfo property, object target)
    {
        Property = property;
        Target = target;
        
        Value = property.GetValue(target);
    }
}



