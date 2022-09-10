import React, { Component } from 'react';

export class FetchPosts extends Component {
  static displayName = FetchPosts.name;

  constructor(props) {
    super(props);
    this.state = { posts: [], loading: true };
  }

  componentDidMount() {
    this.populatePosts();
  }

  static renderPostsTable(posts) {
    return (
      <table className='table table-striped' aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Id</th>
            <th>Title</th>
            <th>Body</th>
          </tr>
        </thead>
        <tbody>
          {posts.map(post =>
            <tr key={post.id}>
              <td>{post.id}</td>
              <td>{post.title}</td>
              <td>{post.body}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchPosts.renderPostsTable(this.state.posts);

    return (
      <div>
        <h1 id="tableLabel" >Blog Posts</h1>
        <p>This component demonstrates fetching blog posts from the server.</p>
        {contents}
      </div>
    );
  }

  async populatePosts() {
    const response = await fetch('api/posts');
    const data = await response.json();
    this.setState({ posts: data, loading: false });
  }
}
