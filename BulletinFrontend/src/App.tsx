import { useAuth0 } from '@auth0/auth0-react'
import './App.css'
import BulletinBoard from './BulletinBoard'
import LoginButton from './LoginButton'
import LogoutButton from './LogoutButton'

function App() {
  const { isAuthenticated } = useAuth0();
  return (
    <div className="App">
      {isAuthenticated && (<LogoutButton></LogoutButton>)}
      {!isAuthenticated && (<LoginButton></LoginButton>)}
      <BulletinBoard></BulletinBoard>
    </div>
  )
}

export default App
