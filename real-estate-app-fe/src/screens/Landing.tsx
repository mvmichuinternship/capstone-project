import React, { useState } from 'react';
import { Link } from 'react-router-dom';

const Landing = () => {

  return (
    // <div className="pl-[6%] min-h-screen w-full flex flex-col">
      <div className="flex-grow flex flex-col justify-center items-center  p-4">
        <h1 className="text-4xl font-bold mb-4">Welcome to RealEstateApp</h1>
        <p className="text-lg mb-8 text-center">Your one-stop solution for all real estate needs. Explore the best properties available in the market.</p>
        <div className="flex space-x-4">
          <Link to="/register" className="bg-blue-400 text-white px-4 py-2 rounded hover:bg-blue-700">Sign Up</Link>
          <Link to="/login" className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-700">Login</Link>
        </div>
      </div>
    // </div>
  );
};

export default Landing;
