using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Pagination
{
    public class PageRequest
    {
        public PageRequest() { }
        public PageRequest(int i, int c) 
        {
            Count = c;
            Index = i;
        }
        public int Count { get; set; }
        public int Index { get; set; }  
    }
}
