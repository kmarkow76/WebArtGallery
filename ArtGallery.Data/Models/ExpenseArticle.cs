namespace ArtGallery.Data.Models;

/// <summary>
/// Класс модели для сущности ExpenseArticle.
/// Представляет статью расходов, связанную с финансовыми операциями.
/// </summary>
public class ExpenseArticle
{
    /// <summary>
    /// Уникальный идентификатор статьи расходов.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Тип или название статьи расходов.
    /// </summary>
    public string ExpenseType { get; set; }

    /// <summary>
    /// Список расходов, связанных с данной статьёй расходов.
    /// </summary>
    public List<MoneyExpense> MoneyExpenses { get; set; }
}