namespace DocumentacaoSwagger.Models
{
    public class Serie
    {

        /// <summary>
        /// Identificador da Serie
        /// <example> 6 </example>
        /// </summary>
        public int SerieId { get; set; }

        /// <summary>
        /// Nome da serie
        /// <example> Star Trek: Guerra nas estrelas </example>
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Historia da serie
        /// <example> Tudo começou quando uma estrela super massiva explodiu... </example>
        /// </summary>
        public string Sinopse { get; set; }

        /// <summary>
        /// Identificador da temporada
        /// <example> 4ª Temporada </example>
        /// </summary>
        public int Temporada { get; set; }

        /// <summary>
        /// Indetificador do numero de episodios que a serie possui
        /// <example> 6 episodios </example>
        /// </summary>
        public int NumeroEpisodios { get; set; }

        /// <summary>
        /// Identificador dos participantes da serie
        /// <example> Maria Albergue, Shako Takamoto, Suzana Viera </example>
        /// </summary>
        public string Elenco { get; set; }

        /// <summary>
        /// Identificador de classificação da serie
        /// <example> Luta, Romance, Guerra </example>
        /// </summary>
        public string Categoria { get; set; }
    }
}
