import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import LoginButton from './LoginButton'
import Profile from './Profile'
import { useAuth0 } from '@auth0/auth0-react'
import SubmitBulletinBoardMessageForm from './SubmitBulletinBoardMessageForm'
import BulletinBoard from './BulletinBoard'
import LogoutButton from './LogoutButton'

function App() {
  const [count, setCount] = useState(0)
  const {user, isAuthenticated, getAccessTokenSilently} = useAuth0();
  const [myAccessToken, setMyAccessToken] = useState("");

  const handleButtonClick = async () => {
    const accessToken = await getAccessTokenSilently();
    console.log("accesstoken", accessToken);
    setMyAccessToken(accessToken);
  };

  return (
    <div className="App">
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src={viteLogo} className="logo" alt="Vite logo" />
        </a>
        <a href="https://reactjs.org" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      {isAuthenticated && (<LogoutButton></LogoutButton>)}
      {!isAuthenticated && (<LoginButton></LoginButton>)}
      <Profile></Profile>
      <button onClick={handleButtonClick}>get access token silently</button>
      <BulletinBoard></BulletinBoard>
    </div>
  )
}

export default App
