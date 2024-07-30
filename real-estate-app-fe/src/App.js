import './App.css';
// import Card from './components/Card.tsx';
import Container from './components/Container.tsx';
// import Button from './components/Button.tsx';
import Register from './screens/Register.tsx';
import PostProperty from './screens/PostProperty.tsx';
import propertyData from './data/propertyData.ts';
import PropertyCard from './components/Property-card.tsx';
import ViewProperty from './screens/ViewProperty.tsx';
import MyProperties from './screens/MyProperties.tsx';
import Landing from './screens/Landing.tsx';
import Login from './screens/Login.tsx'
import SingleProperty from './screens/SingleProperty.tsx';
import NoPage from './screens/NoPage.tsx';
import { Routes, Route, Link, BrowserRouter } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <Container className='h-screen'> 
      {/* <Card className='border border-blue-500'>
      <div>hello world</div>
      <div>hello world</div>
      <div>hello world</div>
      <Button title='click' onClick={() => console.log("first")} />
    </Card> */}
    {/* <Register/> */}
        {/* <PostProperty/> */}
        {/* <ViewProperty/> */}
        <BrowserRouter>
      <Routes>
        <Route path="/" element={<Landing />} />
          <Route path="/register" element={<Register />} />
          <Route path="/login" element={<Login />} />
          <Route path="/post-property" element={<PostProperty />} />
          <Route path="/view-properties" element={<ViewProperty />} />
          <Route path="/my-properties" element={<MyProperties />} />
          <Route path="/view-property/:pid" element={<SingleProperty />} />
          <Route path="*" element={<NoPage />} />
      </Routes>
    </BrowserRouter>
    </Container>
   
     

    </div>
  );
}


export default App;
