import './App.css';
// import Card from './components/Card.tsx';
import Container from './components/Container.tsx';
// import Button from './components/Button.tsx';
import Register from './screens/Register.tsx';
import propertyData from './data/propertyData.ts';
import PropertyCard from './components/Property-card.tsx';

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
    <Register/>
    </Container>
    <PropertyCard propertyData = {propertyData} />
     

    </div>
  );
}

export default App;
