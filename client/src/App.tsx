import React from 'react';
import './App.css';
import axios from 'axios';

class App extends React.Component {
  state = {
    posts: []
}

componentDidMount() {
  axios.get('http://localhost:5000/api/posts')
  .then((response) => {
    this.setState({
      posts: response.data
    })
})
  .catch((error) => {
    console.error(`Error fetching data: ${error}`);
  })
}

  render(){
    const { posts } = this.state;

    return (
    <div className="App">
      <header className="App-header">
        BlogBox
      </header>
      <main>
        {posts.map((post: any) =>
        <div key={post.id}>
          <h1>{post.title}</h1>
          <p>{post.body}</p>
          </div>
        )}
        </main>
    </div>
    );
  }
}

export default App;