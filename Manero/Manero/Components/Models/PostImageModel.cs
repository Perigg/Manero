using Microsoft.AspNetCore.Components.Forms;

namespace Manero.Components.Models
{
    public class PostImageModel
    {
        public IBrowserFile File { get; set; }
        public string UserId { get; set; }
    }
}
