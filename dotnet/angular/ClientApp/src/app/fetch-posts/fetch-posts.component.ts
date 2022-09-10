import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-posts.component.html'
})
export class FetchPostsComponent {
  public posts: BlogPost[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    const url = `${baseUrl}api/posts`;
    http.get<BlogPost[]>(url).subscribe({
      next: result => { this.posts = result; },
      error: err => console.error(err)
    });
  }
}

interface BlogPost {
  id: number;
  title: string;
  body: string;
}
