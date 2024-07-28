import React from 'react'

const Button = ({title, onClick} : {title: string, onClick: (e: React.MouseEvent<HTMLButtonElement>)=> void}) => {
  return (
    <div className=" ">
        <button className='bg-blue-400 text-white rounded-md p-1 w-full hover:bg-blue-500 transition' onClick={onClick}>
        {title}
        </button>
    </div>
  )
}

export default Button