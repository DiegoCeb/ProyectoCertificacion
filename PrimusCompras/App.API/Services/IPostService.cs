using App.API.Models;
using System;
using System.Collections.Generic;

namespace App.API.Services
{
    public interface IPostService
    {
        List<Post> GetAll();
        Post GetById(Guid id);
        bool Create(Post newPost);
        bool Update(Post postToUpdate, Guid id);

    }
}
