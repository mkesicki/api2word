using System.Collections.Generic;

public class CollectionInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public string Uid { get; set; }
}

public class CollectionList {

    public List<CollectionInfo> Collections { get; set; }
}