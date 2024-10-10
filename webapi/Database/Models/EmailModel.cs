public record EmailModel
{
    public required string EmailSender { get; set; }
    public required string EmailTopic { get; set; }
    public required string EmailContent { get; set; }
}