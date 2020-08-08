using App.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.API.Services
{
    public class PostServiceMock : IPostService
    {
        private List<Post> Posteos;

        public PostServiceMock()
        {
            Posteos = new List<Post>();

            for (int i = 0; i < 5; i++)
            {
                Posteos.Add(new Post() { Id = Guid.NewGuid(), Name = "Nombre-" + i });
            }
        }

        public bool Create(Post newPost)
        {
            var exist = Posteos.Any(x => x.Id == newPost.Id);

            if (exist) return false;

            Posteos.Add(newPost);

            return true;
        }

        public List<Post> GetAll()
        {
            return Posteos;
        }

        public Post GetById(Guid id)
        {
            var post = Posteos.FirstOrDefault(x => x.Id == id);

            return post;
        }

        public bool Update(Post postToUpdate, Guid id)
        {
            var post = Posteos.FirstOrDefault(x => x.Id == id);

            if (post == null) return false;

            post.Id = postToUpdate.Id;
            post.Name = postToUpdate.Name;

            return true;
        }
    }
}
