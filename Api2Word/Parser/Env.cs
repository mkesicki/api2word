using System.Collections.Generic;

public class Value
{
    public bool Enabled { get; set; }
    public string Key { get; set; }
    public string value { get; set; }
    public string Type { get; set; }
}

public class Environment
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<Value> Values { get; set; }
}

public class EnvironmentBody
{
    public Environment Environment { get; set; }
}