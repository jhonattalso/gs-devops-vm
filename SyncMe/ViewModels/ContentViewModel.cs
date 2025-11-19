using Microsoft.AspNetCore.Mvc.Rendering;
using SyncMe.Models;
using System.ComponentModel.DataAnnotations;

namespace SyncMe.ViewModels {
    public class ContentViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [Display(Name = "Título do Conteúdo")]
        public string Title { get; set; }

        [Required(ErrorMessage = "O resumo é obrigatório.")]
        [Display(Name = "Resumo")]
        public string Summary { get; set; }

        [Display(Name = "Corpo do Artigo")]
        public string? ArticleBody { get; set; } // Texto longo do blog

        [Display(Name = "URL da Imagem de Capa")]
        public string? CoverImageUrl { get; set; } // URL da imagem de capa


        [Display(Name = "Link da Mídia (YouTube/Imagem)")]
        public string? MediaUrl { get; set; }

        [Display(Name = "Nível de Dificuldade")]
        public DifficultyLevel Difficulty { get; set; }

        // Dropdown de Categoria
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        // Lista para preencher o <select> na View
        public SelectList? Categories { get; set; }

        // Dropdown de Trilha (Opcional)
        [Display(Name = "Trilha de Aprendizado")]
        public int? TrackId { get; set; }
        public SelectList? Tracks { get; set; }
    }
}