namespace CanvasChronicles.Models; 

public class BlogPost 
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string Author { get; set; }
}