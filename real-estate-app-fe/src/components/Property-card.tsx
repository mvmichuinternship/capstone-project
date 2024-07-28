import React from 'react';
import { PropertyDataType } from '../data/propertyData.ts';
import cn from 'clsx';

const PropertyCard = ({ propertyData, className }: { propertyData: PropertyDataType[], className?: string }) => {
  const propertyDataOne = propertyData[0]; // Assuming only one property is passed for now

  return (
    <div className={cn('w-full max-w-sm shadow-xl rounded-xl m-2 p-4 flex flex-col bg-white', className)}>
      <div className='w-full h-64 overflow-hidden rounded-t-lg'>
        <img
          src={`/${propertyDataOne.media[0].url}`}
          alt={propertyDataOne.name}
          className="w-full h-full object-cover"
          onError={(e) => { e.currentTarget.src = '/placeholder.jpg'; }} // Fallback to placeholder if image not found
        />
      </div>
      <div className='w-full flex flex-col p-4'>
        <div className='flex justify-between mb-2'>
          <span className='text-lg font-bold'>{propertyDataOne.name}</span>
          <span className='text-lg font-bold'>${propertyDataOne.price}</span>
        </div>
        <div className='mb-2'>
          <span className='text-sm text-gray-600'>{propertyDataOne.location}</span>
        </div>
        <div className='flex space-x-2'>
          <span className='px-2 py-1 bg-blue-100 text-blue-700 rounded-full text-xs'>
            {propertyDataOne.propertyDetails.commercialAreaInSqFt} sq.ft
          </span>
          {propertyDataOne.commercialSubtype && (
            <span className='px-2 py-1 bg-green-100 text-green-700 rounded-full text-xs'>
              {propertyDataOne.commercialSubtype}
            </span>
          )}
          {propertyDataOne.residentialSubtype && (
            <span className='px-2 py-1 bg-green-100 text-green-700 rounded-full text-xs'>
              {propertyDataOne.residentialSubtype}
            </span>
          )}
        </div>
      </div>
    </div>
  );
};

export default PropertyCard;
