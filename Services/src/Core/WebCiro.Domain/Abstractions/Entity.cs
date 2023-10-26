﻿namespace WebCiro.Domain.Abstractions;
public abstract class Entity
{
    public Entity() { }

    public Entity(string id)
    {
        Id = id;
    }
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsDelete { get; set; } = false;
    public DateTime? DeletedDate { get; set; }
}