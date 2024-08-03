import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const Landing = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLogout = () => {
    setIsLoggedIn(false);
  };

  return (
    // <div className="pl-[6%] min-h-screen w-full flex flex-col">
    //   <nav className="bg-gray-800 p-4">
    //     <div className="  flex justify-between items-center">
    //       <Link to="/" className="text-white text-2xl font-bold">RealEstateApp</Link>
    //       <div>
    //         {isLoggedIn ? (
    //           <button onClick={handleLogout} className="text-white bg-red-500 px-4 py-2 rounded hover:bg-red-700">Signout</button>
    //         ) : (
    //           <>
    //             <Link to="/login" className="text-white bg-blue-500 px-4 py-2 rounded hover:bg-blue-700 mr-2">Login</Link>
    //             <Link to="/register" className="text-white bg-green-500 px-4 py-2 rounded hover:bg-green-700">Signup</Link>
    //           </>
    //         )}
    //       </div>
    //     </div>
    //   </nav>
      <div className="flex-grow flex flex-col justify-center items-center  p-4">
        <h1 className="text-4xl font-bold mb-4">Welcome to RealEstateApp</h1>
        <p className="text-lg mb-8 text-center">Your one-stop solution for all real estate needs. Explore the best properties available in the market.</p>
        <div className="flex space-x-4">
          <Link to="/view-properties" className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-700">View Properties</Link>
          <Link to="/post-property" className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-700">Post Properties</Link>
        </div>
      </div>
    // </div>
  );
};

export default Landing;
