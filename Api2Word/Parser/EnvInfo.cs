using System.Collections.Generic;

public class EnvInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public string Uid { get; set; }
}

public class EnvList
{
    public List<EnvInfo> Environments { get; set; }
}