import "./App.css";
// import Card from './components/Card.tsx';
import Container from "./components/Container.tsx";
// import Button from './components/Button.tsx';
import Register from "./screens/Register.tsx";
import PostProperty from "./screens/PostProperty.tsx";
import propertyData from "./data/propertyData.ts";
import PropertyCard from "./components/Property-card.tsx";
import ViewProperty from "./screens/ViewProperty.tsx";
import MyProperties from "./screens/MyProperties.tsx";
import Landing from "./screens/Landing.tsx";
import Login from "./screens/Login.tsx";
import SingleProperty from "./screens/SingleProperty.tsx";
import NoPage from "./screens/NoPage.tsx";
import { Routes, Route, Link, BrowserRouter } from "react-router-dom";
import Sidebar from "./components/Sidebar.tsx";
import UpgradePlan from "./screens/UpgradePlan.tsx";
import SwitchRole from "./screens/SwitchRoles.tsx";
import EditProperty from "./screens/EditProperty.tsx";
import Navbar from "./components/Navbar.tsx";

function App() {
  return (
    <div className="App h-screen">
      <BrowserRouter>
        <div className="flex h-full overflow-auto ">
          <Sidebar />
          <div className="flex flex-col w-full h-full">
            <div className="ml-[7%]">
              <Navbar />
            </div>
            <Container className="pl-[7%]">
              <Routes>
                <Route path="/" element={<Landing />} />
                <Route path="/register" element={<Register />} />
                <Route path="/login" element={<Login />} />
                <Route path="/post-property" element={<PostProperty />} />
                <Route path="/view-properties" element={<ViewProperty />} />
                <Route path="/my-properties" element={<MyProperties />} />
                <Route
                  path="/view-property/:pid"
                  element={<SingleProperty />}
                />
                <Route path="/edit-property/:pid" element={<EditProperty />} />
                <Route path="/upgrade" element={<UpgradePlan />} />
                <Route path="/switch-roles" element={<SwitchRole />} />
                <Route path="*" element={<NoPage />} />
              </Routes>
            </Container>
          </div>
        </div>
      </BrowserRouter>
    </div>
  );
}

export default App;
