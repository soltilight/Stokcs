using System.ComponentModel.DataAnnotations;

namespace StocksOperation.DTO.DTO_COMMENTS
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must have at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Title should not have more then 280 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must have at least 5 characters")]
        [MaxLength(280, ErrorMessage = "Content should not have more then 280 characters")]
        public string Content { get; set; } = string.Empty;
    }
}
