B. React Core (Next.js se pehle zaroor)

Ye tumhari list me already the, aur Next.js me bhi yahi base hai:

Components, JSX
Props aur children
useState
Events
Conditional rendering
Lists aur key
Controlled forms
useEffect, dependency array aur cleanup
useRef 
Context API
C. Tumhari list me nahi tha, par Next.js ke liye zaroori hai
Component lifecycle: mount, update, unmount ka matlab. Server aur Client Components samajhne me ye kaam aata hai.
Lifting state up aur component composition: children ko pass karna, kyunki Server Component ke andar Client Component rakhne ka pattern yahi hai.
Custom hooks: apna useAuth() ya useFetch() banana.
useReducer: complex state ke liye.
useCallback, useMemo, React.memo: basic idea.
Suspense aur Error Boundaries: Next.js ka loading.tsx aur error.tsx isi par bane hain.
Rendering aur hydration ka concept: React component pehle server pe HTML banta hai, phir browser me “hydrate” hota hai. Hydration errors Next.js me bahut aate hain.
TypeScript with React: props types, useState<Type>(), event types.
D. React 19 ke naye hooks (Next.js 15/16 inhe use karta hai)
use(): Promise ya Context read karne ke liye
useActionState, useFormStatus: Server Actions ke saath forms me
useOptimistic: UI turant update karna, response ka wait kiye bina
useTransition: non-urgent updates
E. Skip kar sakte ho (Next.js me alag tareeke se hota hai)
React Router: Next.js me file-based routing hai (folder = route). React Router ka idea pata ho to kaafi hai, deep seekhne ki zaroorat nahi.
Axios aur useEffect se data fetching: Next.js me Server Components me seedha async/await se data aata hai. Client side fetching ke liye baad me TanStack Query dekh sakte ho.
Class components: purane hain, skip.
Redux: abhi nahi, baad me.