import { useAuth0 } from "@auth0/auth0-react";
import { useEffect, useState } from "react";
import { IBulletinBoardMessage } from "./BulletinBoard";

export interface ISubmit {
    onAddMessage: (message: IBulletinBoardMessage) => void;
}

const SubmitBulletinBoardMessageForm = ({onAddMessage}: ISubmit) => {
    const { user, isAuthenticated, getAccessTokenSilently } = useAuth0();
    const [myAccessToken, setMyAccessToken] = useState("");
    const [message, setMessage] = useState("");

    useEffect(() => {
        const fetchToken = async () => {
            return await getAccessTokenSilently();
        }
        fetchToken().then(token => {
            setMyAccessToken(token);
            console.log("token", token);
        });
    }, [0]);

    const onHandleSubmit = (e: any) => {
        e.preventDefault();
        const url = import.meta.env.VITE_API_URL + "/bulletinboard";
        console.log("url to post to", url);
        fetch(url, {
            body: `message=${encodeURIComponent(message)}`,
            headers: {
                Authorization: `Bearer ${myAccessToken}`,
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            mode: 'cors',
            method: 'POST'
        }).then((r) => {
            console.log('status code', r.status);
            setMessage("");
            return r.json();
        }).then(json => {
             console.log("response json", json);
             onAddMessage(json);
        })
        .catch(e => {
            console.log("oh no, exception", e);
        });
    };

    return (
        <form onSubmit={onHandleSubmit}>
            <label>
                Melding:
                <input type="text" value={message} onChange={(ev) => setMessage(ev.target.value)}></input>
            </label>
            <input type="submit" value="Send"/>
        </form>
    );
}

export default SubmitBulletinBoardMessageForm;