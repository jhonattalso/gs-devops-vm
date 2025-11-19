using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SyncMe.Models {
    [Table("TB_CONTENT")]
    public class Content {
        [Key]
        [Column("ID_CONTENT")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório")]
        [StringLength(100)]
        [Column("NM_TITLE")]
        public string Title { get; set; }

        // Resumo curto para aparecer no Card
        [Required(ErrorMessage = "O resumo é obrigatório")]
        [StringLength(300, ErrorMessage = "O resumo deve ter no máximo 300 caracteres")]
        [Column("DS_SUMMARY")]
        public string Summary { get; set; }

        // --- NOVO: Corpo do Artigo (Texto Longo / Blog) ---
        // Sem StringLength, o banco entende que pode ser texto muito longo
        [Column("DS_ARTICLE_BODY")]
        public string? ArticleBody { get; set; }

        // --- NOVO: Imagem de Capa Opcional ---
        [Column("DS_COVER_IMAGE_URL")]
        public string? CoverImageUrl { get; set; }

        [Column("DS_MEDIA_URL")]
        public string? MediaUrl { get; set; } // Link do YouTube

        [Required]
        [Column("DT_PUBLISH")]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Column("TP_DIFFICULTY")]
        public DifficultyLevel Difficulty { get; set; }

        // --- RELACIONAMENTOS ---

        [Required(ErrorMessage = "Selecione uma categoria")]
        [Column("ID_CATEGORY")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Column("ID_TRACK")]
        public int? TrackId { get; set; }

        [ForeignKey("TrackId")]
        public Track? Track { get; set; }
    }

    public enum DifficultyLevel {
        Iniciante = 0,
        Intermediario = 1,
        Avancado = 2
    }
}