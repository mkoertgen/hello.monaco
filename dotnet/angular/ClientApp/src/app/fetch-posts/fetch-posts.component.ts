import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-posts.component.html'
})
export class FetchPostsComponent {
  public posts: BlogPost[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<BlogPost[]>(baseUrl + 'posts').subscribe(result => {
      this.posts = result;
    }, error => console.error(error));
  }
}

interface BlogPost {
  id: number;
  title: string;
  body: string;
}
